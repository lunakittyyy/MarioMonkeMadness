﻿using MarioMonkeMadness;
using System.Linq;
using UnityEngine;

namespace LibSM64
{
    public class SM64Mario : MonoBehaviour
    {
        Material vertexMaterial, surfaceMaterial;
        SM64InputProvider inputProvider;

        Vector3[][] positionBuffers;
        Vector3[][] normalBuffers;
        Vector3[] lerpPositionBuffer;
        Vector3[] lerpNormalBuffer;
        Vector3[] colorBuffer;
        Color[] colorBufferColors;
        Vector2[] uvBuffer;
        int buffIndex;
        Interop.SM64MarioState[] states;

        GameObject marioRendererObject;
        Mesh marioMesh;
        uint marioId;

        internal void OnEnable()
        {
            SM64Context.RegisterMario(this);

            var initPos = transform.position;
            marioId = Interop.MarioCreate(new Vector3(-initPos.x, initPos.y, initPos.z) * RefCache.Scale);

            inputProvider = GetComponent<SM64InputProvider>();
            if (inputProvider == null)
                throw new System.Exception("Need to add an input provider component to Mario");

            marioRendererObject = new GameObject("MARIO");
            marioRendererObject.hideFlags |= HideFlags.HideInHierarchy;

            var renderer = marioRendererObject.AddComponent<MeshRenderer>();
            var meshFilter = marioRendererObject.AddComponent<MeshFilter>();

            states = new Interop.SM64MarioState[2] {
                new(),
                new()
            };

            vertexMaterial = new Material(RefCache.AssetLoader.GetAsset<Shader>("Shader Graphs/VertexColourShader"));
            surfaceMaterial = new Material(RefCache.AssetLoader.GetAsset<Shader>("Shader Graphs/MarioSurfaceShader"));
            surfaceMaterial.SetTexture("_MainTex", Interop.MarioTexture);
            renderer.materials = new Material[] { vertexMaterial, surfaceMaterial }; 

            marioRendererObject.transform.localScale = new Vector3(-1, 1, 1) / RefCache.Scale;
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
        }

        internal void OnDisable()
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

        public void ContextFixedUpdate()
        {
            Interop.SM64MarioInputs inputs = new();
            Vector3 look = inputProvider.GetCameraLookDirection();
            look.y = 0;
            look = look.normalized;

            Vector2 joystick = inputProvider.GetJoystickAxes();

            inputs.camLookX = -look.x;
            inputs.camLookZ = look.z;
            inputs.stickX = joystick.x;
            inputs.stickY = -joystick.y;
            inputs.buttonA = inputProvider.GetButtonHeld(SM64InputProvider.Button.Jump) ? (byte)1 : (byte)0;
            inputs.buttonB = inputProvider.GetButtonHeld(SM64InputProvider.Button.Kick) ? (byte)1 : (byte)0;
            inputs.buttonZ = inputProvider.GetButtonHeld(SM64InputProvider.Button.Stomp) ? (byte)1 : (byte)0;

            states[buffIndex] = Interop.MarioTick(marioId, inputs, positionBuffers[buffIndex], normalBuffers[buffIndex], colorBuffer, uvBuffer);

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

        public void ContextUpdate()
        {
            float t = RefCache.Config.Interpolation.Value ? (Time.time - Time.fixedTime) / Time.fixedDeltaTime : 1f;
            int j = 1 - buffIndex;

            for (int i = 0; i < lerpPositionBuffer.Length; ++i)
            {
                lerpPositionBuffer[i] = Vector3.LerpUnclamped(positionBuffers[buffIndex][i], positionBuffers[j][i], t);
                lerpNormalBuffer[i] = Vector3.LerpUnclamped(normalBuffers[buffIndex][i], normalBuffers[j][i], t);
            }

            transform.position = Vector3.LerpUnclamped(states[buffIndex].unityPosition, states[j].unityPosition, t);

            marioMesh.vertices = lerpPositionBuffer;
            marioMesh.normals = lerpNormalBuffer;

            marioMesh.RecalculateBounds();
            marioMesh.RecalculateTangents();
        }
    }
}