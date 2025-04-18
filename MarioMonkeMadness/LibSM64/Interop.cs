using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace LibSM64
{
    public static class Interop
    {
        public const float SCALE_FACTOR = 150.0f;

        public const int SM64_TEXTURE_WIDTH  = 64 * 16;
        public const int SM64_TEXTURE_HEIGHT = 64;
        public const int SM64_GEO_MAX_TRIANGLES = 1024;

        public const int SM64_MAX_HEALTH = 8;

        public static Color32[] defaultColors = {
	        new Color32(0  , 0  , 255, 255), // Overalls
	        new Color32(255, 0  , 0  , 255), // Shirt/Hat
                new Color32(254, 193, 121, 255), // Skin
	        new Color32(115, 6  , 0  , 255), // Hair
	        new Color32(255, 255, 255, 255), // Gloves
	        new Color32(114, 28 , 14 , 255), // Shoes
        };

        struct alphaRemovalArea
        {
            public alphaRemovalArea(int _x, int _y, int _w, int _h, int col)
            {
                x = _x;
                y = _y;
                w = _w;
                h = _h;
                colorIndex = col;
            }

            public int x;
            public int y;
            public int w;
            public int h;
            public int colorIndex;
        }
        // welcome to hardcode town
        static alphaRemovalArea[] removalAreas = {
            new alphaRemovalArea(64,     0, 32, 32, 0), // Shirt buttons
            new alphaRemovalArea(128-16, 0, 64, 64, 1), // M cap logo
            new alphaRemovalArea(192-16, 0, 64, 64, 2), // Side hair
            new alphaRemovalArea(256-16, 0, 64, 64, 2), // Moustache
            new alphaRemovalArea(320-16, 0, 64, 64, 2), // Eyes (Normal)
            new alphaRemovalArea(384-16, 0, 64, 64, 2), // Eyes (Half-blink)
            new alphaRemovalArea(448-16, 0, 64, 64, 2), // Eyes (Closed)
            new alphaRemovalArea(512-16, 0, 64, 64, 2), // Eyes (Dead)
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct SM64Surface
        {
            public short type;
            public short force;
            public ushort terrain;
            public int v0x, v0y, v0z;
            public int v1x, v1y, v1z;
            public int v2x, v2y, v2z;
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
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] position;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] velocity;
            public float forwardVel;
            public float faceAngle;
            public float twirlYaw;
            public short health;
            public uint action;
            public uint actionArg;
            public uint actionState;
            public uint actionTimer;
            public uint flags;
            public uint particleFlags;
            public short invincTimer;
            public byte hurtCounter;

            public short animID;
            public short animFrame;
            public ushort animTimer;
            public int animAccel;
            public short animStartFrame;
            public short animLoopStart;
            public short animLoopEnd;

            public Vector3 unityPosition {
                get { return position != null ? new Vector3( -position[0], position[1], position[2] ) / SCALE_FACTOR : Vector3.zero; }
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
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            float[] position;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            float[] eulerRotation;

            static public SM64ObjectTransform FromUnityWorld( Vector3 position, Quaternion rotation )
            {
                float[] vecToArr( Vector3 v )
                {
                    return new float[] { v.x, v.y, v.z };
                }

                float fmod( float a, float b )
                {
                    return a - b * Mathf.Floor( a / b );
                }
                
                float fixAngle( float a )
                {
                    return fmod( a + 180.0f, 360.0f ) - 180.0f;
                }

                var pos = SCALE_FACTOR * Vector3.Scale( position, new Vector3( -1, 1, 1 ));
                var rot = Vector3.Scale( rotation.eulerAngles, new Vector3( -1, 1, 1 ));

                rot.x = fixAngle( rot.x );
                rot.y = fixAngle( rot.y );
                rot.z = fixAngle( rot.z );

                return new SM64ObjectTransform {
                    position = vecToArr( pos ),
                    eulerRotation = vecToArr( rot )
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

        [DllImport("sm64")]
        static extern void sm64_global_init( IntPtr rom, IntPtr outTexture );
        [DllImport("sm64")]
        static extern void sm64_global_terminate();

        [DllImport("sm64")]
        static extern void sm64_audio_init( IntPtr rom );
        [DllImport("sm64")]
        static extern uint sm64_audio_tick(uint numQueuedSamples, uint numDesiredSamples, short[] audio_buffer);

        [DllImport("sm64")]
        static extern void sm64_static_surfaces_load( SM64Surface[] surfaces, ulong numSurfaces );

        [DllImport("sm64")]
        static extern int sm64_mario_create( float marioX, float marioY, float marioZ );
        [DllImport("sm64")]
        static extern void sm64_mario_tick( int marioId, ref SM64MarioInputs inputs, ref SM64MarioState outState, ref SM64MarioGeometryBuffers outBuffers );
        [DllImport("sm64")]
        static extern void sm64_mario_delete( int marioId );

        [DllImport("sm64")]
        static extern void sm64_set_mario_action(int marioId, uint action);
        [DllImport("sm64")]
        static extern void sm64_set_mario_action_arg(int marioId, uint action, uint actionArg);
        [DllImport("sm64")]
        static extern void sm64_set_mario_animation(int marioId, int animID);
        [DllImport("sm64")]
        static extern void sm64_set_mario_anim_frame(int marioId, short animFrame);
        [DllImport("sm64")]
        static extern void sm64_set_mario_state(int marioId, uint flags);
        [DllImport("sm64")]
        static extern void sm64_set_mario_position(int marioId, float x, float y, float z);
        [DllImport("sm64")]
        static extern void sm64_set_mario_angle(int marioId, float x, float y, float z);
        [DllImport("sm64")]
        static extern void sm64_set_mario_faceangle(int marioId, float y);
        [DllImport("sm64")]
        static extern void sm64_set_mario_velocity(int marioId, float x, float y, float z);
        [DllImport("sm64")]
        static extern void sm64_set_mario_forward_velocity(int marioId, float vel);
        [DllImport("sm64")]
        static extern void sm64_set_mario_invincibility(int marioId, short timer);
        [DllImport("sm64")]
        static extern void sm64_set_mario_water_level(int marioId, int level);
        [DllImport("sm64")]
        static extern void sm64_set_mario_gas_level(int marioId, int level);
        [DllImport("sm64")]
        static extern void sm64_set_mario_health(int marioId, ushort health);
        [DllImport("sm64")]
        static extern void sm64_mario_take_damage(int marioId, uint damage, uint subtype, float x, float y, float z);
        [DllImport("sm64")]
        static extern void sm64_mario_heal(int marioId, byte healCounter);
        [DllImport("sm64")]
        static extern void sm64_mario_kill(int marioId);
        [DllImport("sm64")]
        static extern void sm64_mario_interact_cap(int marioId, uint capFlag, ushort capTime, byte playMusic);
        [DllImport("sm64")]
        static extern void sm64_mario_extend_cap(int marioId, ushort capTime);
        [DllImport("sm64")]
        static extern bool sm64_mario_attack(int marioId, float x, float y, float z, float hitboxHeight);

        [DllImport("sm64")]
        static extern uint sm64_surface_object_create( ref SM64SurfaceObject surfaceObject );
        [DllImport("sm64")]
        static extern void sm64_surface_object_move( uint objectId, ref SM64ObjectTransform transform );
        [DllImport("sm64")]
        static extern void sm64_surface_object_delete( uint objectId );

        [DllImport("sm64")]
        static extern int sm64_surface_find_wall_collision(float[] xPtr, float[] yPtr, float[] zPtr, float offsetY, float radius);
        [DllImport("sm64")]
        static extern float sm64_surface_find_floor_height_and_data(float xPos, float yPos, float zPos);
        [DllImport("sm64")]
        static extern float sm64_surface_find_floor_height(float x, float y, float z);
        [DllImport("sm64")]
        static extern float sm64_surface_find_floor(float xPos, float yPos, float zPos);
        [DllImport("sm64")]
        static extern float sm64_surface_find_water_level(float x, float z);
        [DllImport("sm64")]
        static extern float sm64_surface_find_poison_gas_level(float x, float z);

        [DllImport("sm64")]
        static extern void sm64_seq_player_play_sequence(byte player, byte seqId, ushort arg2);
        [DllImport("sm64")]
        static extern void sm64_play_music(byte player, ushort seqArgs, ushort fadeTimer);
        [DllImport("sm64")]
        static extern void sm64_stop_background_music(ushort seqId);
        [DllImport("sm64")]
        static extern void sm64_fadeout_background_music(ushort arg0, ushort fadeOut);
        [DllImport("sm64")]
        static extern ushort sm64_get_current_background_music();
        [DllImport("sm64")]
        static extern void sm64_play_sound(uint soundBits, float[] pos);
        [DllImport("sm64")]
        static extern void sm64_play_sound_global(uint soundBits);
        [DllImport("sm64")]
        static extern void sm64_set_sound_volume(float vol);


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void DebugPrintFuncDelegate(string str);

        [DllImport("sm64")]
        static extern void sm64_register_debug_print_function(IntPtr debugPrintFunction);

        static public Texture2D defaultTexture { get; private set; }
        static public Texture2D marioTexture { get; private set; }
        static public bool isGlobalInit { get; private set; }

        static void debugPrintCallback(string str)
        {
            Debug.Log("libsm64: " + str);
        }

        public static void GlobalInit( byte[] rom )
        {
            //var debugDelegate = new DebugPrintFuncDelegate( debugPrintCallback );
            var romHandle = GCHandle.Alloc( rom, GCHandleType.Pinned );
            var textureData = new byte[ 4 * SM64_TEXTURE_WIDTH * SM64_TEXTURE_HEIGHT ];
            var textureDataHandle = GCHandle.Alloc( textureData, GCHandleType.Pinned );

            sm64_global_init( romHandle.AddrOfPinnedObject(), textureDataHandle.AddrOfPinnedObject());
            sm64_audio_init( romHandle.AddrOfPinnedObject() );
            //sm64_register_debug_print_function(Marshal.GetFunctionPointerForDelegate(debugDelegate));

            Color32[] cols = new Color32[ SM64_TEXTURE_WIDTH * SM64_TEXTURE_HEIGHT ];
            defaultTexture = new Texture2D( SM64_TEXTURE_WIDTH, SM64_TEXTURE_HEIGHT, TextureFormat.RGBA32, false );
            for (int ix = 0; ix < SM64_TEXTURE_WIDTH; ix++)
            {
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

            defaultTexture.SetPixels32(cols);
            defaultTexture.Apply();

            marioTexture = GenerateTexture(defaultColors);

            romHandle.Free();
            textureDataHandle.Free();

            sm64_play_sound_global(SM64Constants.SOUND_MENU_STAR_SOUND);
            isGlobalInit = true;
        }

        public static void GlobalTerminate()
        {
            sm64_global_terminate();
            marioTexture = null;
            isGlobalInit = false;
        }

        public static Texture2D GenerateTexture(Color32[] customColors)
        {
            Texture2D texture = new Texture2D(SM64_TEXTURE_WIDTH, SM64_TEXTURE_HEIGHT, TextureFormat.RGBA32, false);
            Color32[] cols = defaultTexture.GetPixels32();

            // Store Mario's default colors in the texture.
            // If using a shader that doesn't support vertex colors,
            // this is a viable workaround
            for (int i = 0; i < customColors.Length; i++)
            {
                for (int ix = SM64_TEXTURE_WIDTH - 32 - 1; ix < SM64_TEXTURE_WIDTH; ix++)
                {
                    for (int iy = i * 10; iy < i * 10 + 10; iy++)
                    {
                        cols[ix + SM64_TEXTURE_WIDTH * iy] = customColors[i];
                    }
                }
            }

            // Replace transparency in certain parts of the texture
            // with a solid color
            for (int i = 0; i < removalAreas.Length; i++)
            {
                alphaRemovalArea r = removalAreas[i];
                for (int ix = r.x; ix < r.x + r.w; ix++)
                {
                    for (int iy = r.y; iy < r.y + r.h; iy++)
                    {
                        if (cols[ix + SM64_TEXTURE_WIDTH * iy].a != 0) continue;
                        cols[ix + SM64_TEXTURE_WIDTH * iy] = customColors[r.colorIndex];
                    }
                }
            }

            texture.SetPixels32(cols);
            texture.Apply();

            return texture;
        }

        public static void StaticSurfacesLoad( SM64Surface[] surfaces )
        {
            sm64_static_surfaces_load( surfaces, (ulong)surfaces.Length );
        }

        public static int MarioCreate( Vector3 marioPos )
        {
            return sm64_mario_create( (short)marioPos.x, (short)marioPos.y, (short)marioPos.z );
        }

        public static void MarioTick( int marioId, SM64MarioInputs inputs, ref SM64MarioState outState, Vector3[] positionBuffer, Vector3[] normalBuffer, Vector3[] colorBuffer, Vector2[] uvBuffer )
        {
            var posHandle = GCHandle.Alloc( positionBuffer, GCHandleType.Pinned );
            var normHandle = GCHandle.Alloc( normalBuffer, GCHandleType.Pinned );
            var colorHandle = GCHandle.Alloc( colorBuffer, GCHandleType.Pinned );
            var uvHandle = GCHandle.Alloc( uvBuffer, GCHandleType.Pinned );

            SM64MarioGeometryBuffers buff = new SM64MarioGeometryBuffers
            {
                position = posHandle.AddrOfPinnedObject(),
                normal = normHandle.AddrOfPinnedObject(),
                color = colorHandle.AddrOfPinnedObject(),
                uv = uvHandle.AddrOfPinnedObject()
            };

            sm64_mario_tick( marioId, ref inputs, ref outState, ref buff );

	    //uncomment to make Mario stay in 2D
	    //sm64_set_mario_position(marioId, outState.position[0], outState.position[1], -1*SCALE_FACTOR);

            posHandle.Free();
            normHandle.Free();
            colorHandle.Free();
            uvHandle.Free();
        }

        public static void SetWaterLevel(int marioId, int waterLevel)
        {
            sm64_set_mario_water_level(marioId, waterLevel);
        }
        
        public static void MarioDelete( int marioId )
        {
            sm64_mario_delete( marioId );
        }

        public static void MarioSetPosition(int marioId, Vector3 pos)
        {
            sm64_set_mario_position(marioId, -pos.x * SCALE_FACTOR, pos.y * SCALE_FACTOR, pos.z * SCALE_FACTOR);
        }

        public static void MarioSetVelocity(int marioId, Vector3 vel)
        {
            sm64_set_mario_velocity(marioId, vel.x, vel.y, vel.z);
        }

        public static void MarioSetForwardVelocity(int marioId, float vel)
        {
            sm64_set_mario_forward_velocity(marioId, vel);
        }

        public static void MarioTakeDamage(int marioId, uint damage, uint subtype, Vector3 pos)
        {
            sm64_mario_take_damage(marioId, damage, subtype, -pos.x * SCALE_FACTOR, pos.y * SCALE_FACTOR, pos.z * SCALE_FACTOR);
        }

        public static void MarioSetHealth(int marioId, ushort health)
        {
            sm64_set_mario_health(marioId, health);
        }

        public static void MarioKill(int marioId)
        {
            sm64_mario_kill(marioId);
        }

        public static void MarioSetAngle(int marioId, float x, float y, float z)
        {
            sm64_set_mario_angle(marioId, x, y, z);
        }

        public static void MarioSetFaceAngle(int marioId, float angle)
        {
            sm64_set_mario_faceangle(marioId, angle);
        }

        public static void MarioSetAction(int marioId, SM64Constants.Action action)
        {
            sm64_set_mario_action(marioId, (uint)action);
        }

        public static void MarioSetAction(int marioId, SM64Constants.Action action, uint actionArg)
        {
            sm64_set_mario_action_arg(marioId, (uint)action, actionArg);
        }

        public static void MarioSetAnim(int marioId, SM64Constants.MarioAnimID animID)
        {
            sm64_set_mario_animation(marioId, (int)animID);
        }

        public static void MarioSetAnimFrame(int marioId, short animFrame)
        {
            sm64_set_mario_anim_frame(marioId, animFrame);
        }

        public static bool MarioAttack(int marioId, Vector3 pos, float hitboxHeight)
        {
            return sm64_mario_attack(marioId, -pos.x * SCALE_FACTOR, pos.y * SCALE_FACTOR, pos.z * SCALE_FACTOR, hitboxHeight);
        }

        public static void PlaySound(uint soundBits, Vector3 pos)
        {
            sm64_play_sound(soundBits, new float[] {pos.x, pos.y, pos.z});
        }

        public static void PlaySound(uint soundBits)
        {
            sm64_play_sound_global(soundBits);
        }

        public static uint SurfaceObjectCreate( Vector3 position, Quaternion rotation, SM64Surface[] surfaces )
        {
            var surfListHandle = GCHandle.Alloc( surfaces, GCHandleType.Pinned );
            var t = SM64ObjectTransform.FromUnityWorld( position, rotation );

            SM64SurfaceObject surfObj = new SM64SurfaceObject
            {
                transform = t,
                surfaceCount = (uint)surfaces.Length,
                surfaces = surfListHandle.AddrOfPinnedObject()
            };

            uint result = sm64_surface_object_create( ref surfObj );

            surfListHandle.Free();

            return result;
        }

        public static void SurfaceObjectMove( uint id, Vector3 position, Quaternion rotation )
        {
            var t = SM64ObjectTransform.FromUnityWorld( position, rotation );
            sm64_surface_object_move( id, ref t );
        }

        public static void SurfaceObjectDelete( uint id )
        {
            sm64_surface_object_delete( id );
        }
    }
}
