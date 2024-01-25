using MarioMonkeMadness;
using MarioMonkeMadness.Behaviours;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace LibSM64
{
    public class SM64Mario : MonoBehaviour
    {
        Material vertexMaterial, surfaceMaterial;
        private SM64InputProvider inputProvider;

        private Vector3[][] positionBuffers;
        private Vector3[][] normalBuffers;
        private Vector3[] lerpPositionBuffer;
        private Vector3[] lerpNormalBuffer;
        private Vector3[] colorBuffer;
        private Color[] colorBufferColors;
        private Vector2[] uvBuffer;
        private ushort numTrianglesUsed;
        private int buffIndex;
        private Interop.SM64MarioState[] states;

        private Mesh marioMesh;
        private uint marioId;

        private Vector3 previousVelocity;
        private ushort previousNumTrianglesUsed = 0;

        public Action MarioStartedMoving;
        public Action MarioStoppedMoving;
        public GameObject marioRendererObject;

        public async void OnEnable()
        {
            SM64Context.RegisterMario(this);

            var initPos = transform.position;
            var initRot = transform.eulerAngles;
            marioId = Interop.MarioCreate(new Vector3(-initPos.x, initPos.y, initPos.z) * Interop.SCALE_FACTOR, initRot);

            if (marioId == uint.MaxValue)
            {
                SM64Context.UnregisterMario(this);
                return;
            }
            else
            {
                Debug.Log("Mario spawned outside of a surface. Deleting...");
            }

            inputProvider = GetComponent<SM64InputProvider>();
            if (inputProvider == null)
                throw new System.Exception("Need to add an input provider component to Mario");

            marioRendererObject = new GameObject("MARIO");
            marioRendererObject.hideFlags |= HideFlags.HideInHierarchy;

            var renderer = marioRendererObject.AddComponent<MeshRenderer>();
            var meshFilter = marioRendererObject.AddComponent<MeshFilter>();
            renderer.forceRenderingOff = true;

            states = new Interop.SM64MarioState[2] {
                new(),
                new()
            };

            vertexMaterial = new Material(RefCache.AssetLoader.GetAsset<Shader>("Shader Graphs/VertexColourShader"));
            surfaceMaterial = new Material(RefCache.AssetLoader.GetAsset<Shader>("Shader Graphs/MarioSurfaceShader"));
            surfaceMaterial.SetTexture("_MainTex", Interop.MarioTexture);
            renderer.materials = new Material[] { vertexMaterial, surfaceMaterial };

            marioRendererObject.transform.localScale = new Vector3(-1, 1, 1) / Interop.SCALE_FACTOR;
            marioRendererObject.transform.localPosition = Vector3.zero;

            lerpPositionBuffer = new Vector3[3 * Interop.SM64_GEO_MAX_TRIANGLES];
            lerpNormalBuffer = new Vector3[3 * Interop.SM64_GEO_MAX_TRIANGLES];
            positionBuffers = new Vector3[][] { new Vector3[3 * Interop.SM64_GEO_MAX_TRIANGLES], new Vector3[3 * Interop.SM64_GEO_MAX_TRIANGLES] };
            normalBuffers = new Vector3[][] { new Vector3[3 * Interop.SM64_GEO_MAX_TRIANGLES], new Vector3[3 * Interop.SM64_GEO_MAX_TRIANGLES] };
            colorBuffer = new Vector3[3 * Interop.SM64_GEO_MAX_TRIANGLES];
            colorBufferColors = new Color[3 * Interop.SM64_GEO_MAX_TRIANGLES];
            uvBuffer = new Vector2[3 * Interop.SM64_GEO_MAX_TRIANGLES];

            marioMesh = new Mesh();
            marioMesh.vertices = lerpPositionBuffer;
            marioMesh.triangles = Enumerable.Range(0, 3 * Interop.SM64_GEO_MAX_TRIANGLES).ToArray();
            meshFilter.sharedMesh = marioMesh;

            transform.localScale = new Vector3(Mathf.Pow(MarioMonkeMadness.Constants.TriggerLength, 0.28f), MarioMonkeMadness.Constants.TriggerLength, Mathf.Pow(MarioMonkeMadness.Constants.TriggerLength, 0.28f));
            SetAction(SM64MarioAction.ACT_JUMP);

            for (int i = 0; i < 4; i++)
            {
                SetFowardVelocity(i * 0.021f);
                SetUpwardVelocity(0.21f);
                await Task.Delay(40);

                renderer.forceRenderingOff = false;
            }

            await Task.Delay(1200);
            GameObject healthBar = Instantiate(RefCache.AssetLoader.GetAsset<GameObject>("HealthBar"));
            healthBar.transform.SetParent(transform, false);
            healthBar.transform.localScale = new Vector3(1f / transform.localScale.x, 1f / transform.localScale.y, 1f / transform.localScale.z) / 1.9f;
            healthBar.AddComponent<MarioHealthBar>().Renderer = renderer;

            if (RefCache.IsWingSession) ClaimCap(SM64MarioFlags.MARIO_WING_CAP, ushort.MaxValue);
        }

        public void OnDisable()
        {
            if (marioRendererObject != null)
            {
                Destroy(marioRendererObject);
                marioRendererObject = null;
            }

            if (Interop.IsGlobalInit)
            {
                SM64Context.UnregisterMario(this);
                Interop.MarioDelete(marioId);
            }
        }

        public void SetAction(SM64MarioAction action)
        {
            Interop.MarioSetAction(marioId, action);
        }

        public void SetState(SM64MarioFlags flags)
        {
            Interop.MarioSetState(marioId, flags);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            Interop.MarioSetPosition(marioId, position);
        }

        public void SetRotation(Quaternion rotation)
        {
            Interop.MarioSetRotation(marioId, rotation);
        }

        public void SetRotation(float angle)
        {
            Interop.MarioSetRotation(marioId, angle);
        }

        public void SetVelocity(Vector3 velocity)
        {
            Interop.MarioSetVelocity(marioId, velocity);
        }

        public void SetFowardVelocity(float velocity)
        {
            Interop.MarioSetForwardVelocity(marioId, velocity);
        }

        public void SetUpwardVelocity(float velocity)
        {
            Interop.MarioSetUpwardVelocity(marioId, velocity);
        }

        public void SetInvincibility(short timer)
        {
            Interop.MarioSetInvincibility(marioId, timer);
        }

        public void SetHealth(ushort health)
        {
            Interop.MarioSetHealth(marioId, health);
        }

        public void SetWaterLevel(float level)
        {
            level *= Interop.SCALE_FACTOR;
            Interop.MarioSetWaterLevel(marioId, Mathf.FloorToInt(level - 0.5f));
        }

        public void ClaimCap(uint capFlag, ushort capTime)
        {
            Interop.MarioClaimCap(marioId, capFlag, capTime);
        }

        public void ClaimCap(SM64MarioFlags capFlag, ushort capTime)
        {
            ClaimCap((uint)capFlag, capTime);
        }

        public void RefreshInputProvider()
        {
            inputProvider = GetComponent<SM64InputProvider>();
        }

        internal void ContextFixedUpdate()
        {
            if (!enabled || !gameObject.activeInHierarchy) return;

            var inputs = new Interop.SM64MarioInputs();
            if (inputProvider != null)
            {
                var look = inputProvider.GetCameraLookDirection();
                look.y = 0;
                look = look.normalized;

                var joystick = inputProvider.GetJoystickAxes();

                inputs.camLookX = -look.x;
                inputs.camLookZ = look.z;
                inputs.stickX = joystick.x;
                inputs.stickY = -joystick.y;
                inputs.buttonA = inputProvider.GetButtonHeld(SM64InputProvider.Button.Jump) ? (byte)1 : (byte)0;
                inputs.buttonB = inputProvider.GetButtonHeld(SM64InputProvider.Button.Kick) ? (byte)1 : (byte)0;
                inputs.buttonZ = inputProvider.GetButtonHeld(SM64InputProvider.Button.Stomp) ? (byte)1 : (byte)0;
            }

            states[buffIndex] = Interop.MarioTick(marioId, inputs, positionBuffers[buffIndex], normalBuffers[buffIndex], colorBuffer, uvBuffer, out numTrianglesUsed);

            if (previousNumTrianglesUsed != numTrianglesUsed)
            {
                for (int i = numTrianglesUsed * 3; i < positionBuffers[buffIndex].Length; i++)
                {
                    positionBuffers[buffIndex][i] = Vector3.zero;
                    normalBuffers[buffIndex][i] = Vector3.zero;
                }
                positionBuffers[buffIndex].CopyTo(positionBuffers[1 - buffIndex], 0);
                normalBuffers[buffIndex].CopyTo(normalBuffers[1 - buffIndex], 0);
                positionBuffers[buffIndex].CopyTo(lerpPositionBuffer, 0);
                normalBuffers[buffIndex].CopyTo(lerpNormalBuffer, 0);

                previousNumTrianglesUsed = numTrianglesUsed;
            }

            Color baseColour = GorillaTagger.Instance.offlineVRRig.materialsToChangeTo[0].color;
            for (int i = 0; i < colorBuffer.Length; ++i)
            {
                Color originalColour = new(colorBuffer[i].x, colorBuffer[i].y, colorBuffer[i].z, 1);
                if (RefCache.Config.CustomColour.Value)
                {
                    Color.RGBToHSV(originalColour, out _, out float s, out float v);
                    colorBufferColors[i] = baseColour * Mathf.LerpUnclamped(v, s, s % v);
                }
                else
                {
                    colorBufferColors[i] = originalColour;
                }
            }

            marioMesh.colors = colorBufferColors;
            marioMesh.uv = uvBuffer;

            buffIndex = 1 - buffIndex;
        }

        internal void ContextUpdate()
        {
            if (!enabled || !gameObject.activeInHierarchy) return;

            float t = RefCache.Config.Interpolation.Value ? (Time.time - Time.fixedTime) / Time.fixedDeltaTime : 1f;
            int j = 1 - buffIndex;

            for (int i = 0; i < numTrianglesUsed * 3; ++i)
            {
                lerpPositionBuffer[i] = Vector3.LerpUnclamped(positionBuffers[buffIndex][i], positionBuffers[j][i], t);
                lerpNormalBuffer[i] = Vector3.LerpUnclamped(normalBuffers[buffIndex][i], normalBuffers[j][i], t);
            }

            transform.position = Vector3.LerpUnclamped(states[buffIndex].UnityPosition, states[j].UnityPosition, t);
            transform.rotation = Quaternion.Euler(0f, 360f - states[buffIndex].faceAngle * Mathf.Rad2Deg, 0f);

            marioMesh.vertices = lerpPositionBuffer;
            marioMesh.normals = lerpNormalBuffer;

            marioMesh.RecalculateBounds();
            marioMesh.RecalculateTangents();

            Vector3 velocity = SM64Vec3ToVector3(states[buffIndex].velocity);
            if (MarioStartedMoving != null && previousVelocity.magnitude == 0 && velocity.magnitude > 0)
                MarioStartedMoving.Invoke();
            else if (MarioStoppedMoving != null && velocity.magnitude == 0 && previousVelocity.magnitude > 0)
                MarioStoppedMoving.Invoke();
            previousVelocity = velocity;
        }

        private Vector3 SM64Vec3ToVector3(float[] sm64Vec)
        {
            if (sm64Vec != null && sm64Vec.Length >= 3)
                return new Vector3(sm64Vec[0], sm64Vec[1], sm64Vec[2]);
            return Vector3.zero;
        }
    }
}