using MarioMonkeMadness;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LibSM64
{
    internal static class Interop
    {
        public const int SM64_TEXTURE_WIDTH = 64 * 11;
        public const int SM64_TEXTURE_HEIGHT = 64;
        public const int SM64_GEO_MAX_TRIANGLES = 1024;

        public const int SM64_MAX_HEALTH = 8;

        public const float SM64_DEG2ANGLE = 182.04459f;

        [StructLayout(LayoutKind.Sequential)]
        public struct SM64Surface
        {
            public short type;
            public short force;
            public ushort terrain;
            public short v0x, v0y, v0z;
            public short v1x, v1y, v1z;
            public short v2x, v2y, v2z;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct SM64MarioInputs
        {
            public float camLookX, camLookZ;
            public float stickX, stickY;
            public byte buttonA, buttonB, buttonZ;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct SM64MarioState
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] position;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] velocity;
            public float faceAngle;
            public short health;

            public Vector3 unityPosition
            {
                get { return position != null ? new Vector3(-position[0], position[1], position[2]) / RefCache.Scale : Vector3.zero; }
            }
        };

        [StructLayout(LayoutKind.Sequential)]
        struct SM64MarioGeometryBuffers
        {
            public IntPtr position;
            public IntPtr normal;
            public IntPtr color;
            public IntPtr uv;
            public ushort numTrianglesUsed;
        };

        [StructLayout(LayoutKind.Sequential)]
        struct SM64ObjectTransform
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            float[] position;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            float[] eulerRotation;

            static public SM64ObjectTransform FromUnityWorld(Vector3 position, Quaternion rotation)
            {
                float[] vecToArr(Vector3 v)
                {
                    return new float[] { v.x, v.y, v.z };
                }

                float fmod(float a, float b)
                {
                    return a - b * Mathf.Floor(a / b);
                }

                float fixAngle(float a)
                {
                    return fmod(a + 180.0f, 360.0f) - 180.0f;
                }

                var pos = RefCache.Scale * Vector3.Scale(position, new Vector3(-1, 1, 1));
                var rot = Vector3.Scale(rotation.eulerAngles, new Vector3(-1, 1, 1));

                rot.x = fixAngle(rot.x);
                rot.y = fixAngle(rot.y);
                rot.z = fixAngle(rot.z);

                return new SM64ObjectTransform
                {
                    position = vecToArr(pos),
                    eulerRotation = vecToArr(rot)
                };
            }
        };

        [StructLayout(LayoutKind.Sequential)]
        struct SM64SurfaceObject
        {
            public SM64ObjectTransform transform;
            public uint surfaceCount;
            public IntPtr surfaces;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SM64MarioColorGroup
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] shade;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] color;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SM64MarioModelColors
        {
            public SM64MarioColorGroup blue;
            public SM64MarioColorGroup red;
            public SM64MarioColorGroup white;
            public SM64MarioColorGroup brown1;
            public SM64MarioColorGroup beige;
            public SM64MarioColorGroup brown2;
        }

        [DllImport("sm64")]
        static extern void sm64_global_init(IntPtr rom, IntPtr outTexture, IntPtr debugPrintFunctionPtr);
        [DllImport("sm64")]
        static extern void sm64_global_terminate();

        [DllImport("sm64")]
        static extern void sm64_static_surfaces_load(SM64Surface[] surfaces, ulong numSurfaces);

        [DllImport("sm64")]
        static extern uint sm64_mario_create(short marioX, short marioY, short marioZ, short marioRx, short marioRy, short marioRz);
        [DllImport("sm64")]
        static extern void sm64_mario_tick(uint marioId, ref SM64MarioInputs inputs, ref SM64MarioState outState, ref SM64MarioGeometryBuffers outBuffers);
        [DllImport("sm64")]
        static extern void sm64_mario_delete(uint marioId);

        [DllImport("sm64")]
        static extern void sm64_set_mario_action(uint marioId, uint action);
        [DllImport("sm64")]
        static extern void sm64_set_mario_position(uint marioId, float marioX, float marioY, float marioZ);
        [DllImport("sm64")]
        static extern void sm64_set_mario_angle(uint marioId, short marioX, short marioY, short marioZ);
        [DllImport("sm64")]
        static extern void sm64_set_mario_velocity(uint marioId, float velX, float velY, float velZ);
        [DllImport("sm64")]
        static extern void sm64_set_mario_forward_velocity(uint marioId, float vel);
        [DllImport("sm64")]
        static extern void sm64_set_mario_colors(ref SM64MarioModelColors modelColors);

        [DllImport("sm64")]
        static extern uint sm64_surface_object_create(ref SM64SurfaceObject surfaceObject);
        [DllImport("sm64")]
        static extern void sm64_surface_object_move(uint objectId, ref SM64ObjectTransform transform);
        [DllImport("sm64")]
        static extern void sm64_surface_object_delete(uint objectId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DebugPrintFuncDelegate(string str);

        static public Texture2D MarioTexture { get; private set; }
        static public bool IsGlobalInit { get; private set; }

        static void DebugPrintCallback(string str)
        {
#if DEBUG
            Debug.Log("libsm64: " + str);
#endif
        }

        public static void GlobalInit(byte[] rom)
        {
            var callbackDelegate = new DebugPrintFuncDelegate(DebugPrintCallback);
            var romHandle = GCHandle.Alloc(rom, GCHandleType.Pinned);
            var textureData = new byte[4 * SM64_TEXTURE_WIDTH * SM64_TEXTURE_HEIGHT];
            var textureDataHandle = GCHandle.Alloc(textureData, GCHandleType.Pinned);

            sm64_global_init(romHandle.AddrOfPinnedObject(), textureDataHandle.AddrOfPinnedObject(), Marshal.GetFunctionPointerForDelegate(callbackDelegate));

            Color32[] cols = new Color32[SM64_TEXTURE_WIDTH * SM64_TEXTURE_HEIGHT];
            MarioTexture = new Texture2D(SM64_TEXTURE_WIDTH, SM64_TEXTURE_HEIGHT);
            for (int ix = 0; ix < SM64_TEXTURE_WIDTH; ix++)
            {
                if (ix < 64) continue;
                for (int iy = 0; iy < SM64_TEXTURE_HEIGHT; iy++)
                {
                    cols[ix + SM64_TEXTURE_WIDTH * iy] = new Color32(
                        textureData[4 * (ix + SM64_TEXTURE_WIDTH * iy) + 0],
                        textureData[4 * (ix + SM64_TEXTURE_WIDTH * iy) + 1],
                        textureData[4 * (ix + SM64_TEXTURE_WIDTH * iy) + 2],
                        textureData[4 * (ix + SM64_TEXTURE_WIDTH * iy) + 3]
                    );
                }
            }

            MarioTexture.SetPixels32(cols);
            MarioTexture.Apply();

            // Modify the Mario texture to better fit the model
            MarioTexture.filterMode = FilterMode.Point;
            MarioTexture.wrapMode = TextureWrapMode.Clamp;

            romHandle.Free();
            textureDataHandle.Free();

            IsGlobalInit = true;
        }

        public static void GlobalTerminate()
        {
            sm64_global_terminate();
            MarioTexture = null;
            IsGlobalInit = false;
        }

        public static void StaticSurfacesLoad(SM64Surface[] surfaces)
        {
            sm64_static_surfaces_load(surfaces, (ulong)surfaces.Length);
        }

        public static uint MarioCreate(Vector3 marioPos, Vector3 marioEulerAngles)
        {
            marioEulerAngles = new Vector3(marioEulerAngles.x, 360 - marioEulerAngles.y, marioEulerAngles.z) * SM64_DEG2ANGLE;
            return sm64_mario_create((short)marioPos.x, (short)marioPos.y, (short)marioPos.z, (short)marioEulerAngles.x, (short)marioEulerAngles.y, (short)marioEulerAngles.z);
        }

        public static SM64MarioState MarioTick(uint marioId, SM64MarioInputs inputs, Vector3[] positionBuffer, Vector3[] normalBuffer, Vector3[] colorBuffer, Vector2[] uvBuffer, out ushort numTrianglesUsed)
        {
            SM64MarioState outState = new();

            var posHandle = GCHandle.Alloc(positionBuffer, GCHandleType.Pinned);
            var normHandle = GCHandle.Alloc(normalBuffer, GCHandleType.Pinned);
            var colorHandle = GCHandle.Alloc(colorBuffer, GCHandleType.Pinned);
            var uvHandle = GCHandle.Alloc(uvBuffer, GCHandleType.Pinned);

            SM64MarioGeometryBuffers buff = new()
            {
                position = posHandle.AddrOfPinnedObject(),
                normal = normHandle.AddrOfPinnedObject(),
                color = colorHandle.AddrOfPinnedObject(),
                uv = uvHandle.AddrOfPinnedObject(),
            };

            sm64_mario_tick(marioId, ref inputs, ref outState, ref buff);

            numTrianglesUsed = buff.numTrianglesUsed;

            posHandle.Free();
            normHandle.Free();
            colorHandle.Free();
            uvHandle.Free();

            return outState;
        }

        public static void MarioDelete(uint marioId)
        {
            sm64_mario_delete(marioId);
        }

        public static void MarioSetAction(uint marioId, SM64MarioAction action)
        {
            sm64_set_mario_action(marioId, (uint)action);
        }

        public static void MarioSetPosition(uint marioId, Vector3 position)
        {
            position *= RefCache.Config.MarioScale.Value;
            sm64_set_mario_position(marioId, -position.x, position.y, position.z);
        }

        public static void MarioSetRotation(uint marioId, Quaternion rotation)
        {
            Vector3 eulerAngles = rotation.eulerAngles;
            eulerAngles = new Vector3(eulerAngles.x, 360 - eulerAngles.y, eulerAngles.z);
            eulerAngles *= SM64_DEG2ANGLE;
            sm64_set_mario_angle(marioId, (short)eulerAngles.x, (short)eulerAngles.y, (short)eulerAngles.z);
        }

        public static void MarioSetVelocity(uint marioId, Vector3 velocity)
        {
            velocity *= RefCache.Config.MarioScale.Value;
            sm64_set_mario_velocity(marioId, velocity.x, velocity.y, velocity.z);
        }

        public static void MarioSetForwardVelocity(uint marioId, float velocity)
        {
            velocity *= RefCache.Config.MarioScale.Value;
            sm64_set_mario_forward_velocity(marioId, velocity);
        }

        public static void MarioSetColors(Color32[] unityColors)
        {
            byte[][] colorBytes = new byte[12][];
            for (int i = 0; i < unityColors.Length; i++)
            {
                colorBytes[i] = new byte[3];
                colorBytes[i][0] = unityColors[i].r;
                colorBytes[i][1] = unityColors[i].g;
                colorBytes[i][2] = unityColors[i].b;
            }

            SM64MarioModelColors colors = new()
            {
                blue = new SM64MarioColorGroup { shade = colorBytes[0], color = colorBytes[1] },
                red = new SM64MarioColorGroup { shade = colorBytes[2], color = colorBytes[3] },
                white = new SM64MarioColorGroup { shade = colorBytes[4], color = colorBytes[5] },
                brown1 = new SM64MarioColorGroup { shade = colorBytes[6], color = colorBytes[7] },
                beige = new SM64MarioColorGroup { shade = colorBytes[8], color = colorBytes[9] },
                brown2 = new SM64MarioColorGroup { shade = colorBytes[10], color = colorBytes[11] },
            };

            sm64_set_mario_colors(ref colors);
        }

        public static uint SurfaceObjectCreate(Vector3 position, Quaternion rotation, SM64Surface[] surfaces)
        {
            var surfListHandle = GCHandle.Alloc(surfaces, GCHandleType.Pinned);
            var t = SM64ObjectTransform.FromUnityWorld(position, rotation);

            SM64SurfaceObject surfObj = new()
            {
                transform = t,
                surfaceCount = (uint)surfaces.Length,
                surfaces = surfListHandle.AddrOfPinnedObject()
            };

            uint result = sm64_surface_object_create(ref surfObj);

            surfListHandle.Free();

            return result;
        }

        public static void SurfaceObjectMove(uint id, Vector3 position, Quaternion rotation)
        {
            var t = SM64ObjectTransform.FromUnityWorld(position, rotation);
            sm64_surface_object_move(id, ref t);
        }

        public static void SurfaceObjectDelete(uint id)
        {
            sm64_surface_object_delete(id);
        }
    }
}
