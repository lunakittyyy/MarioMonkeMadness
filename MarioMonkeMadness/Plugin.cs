﻿using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using LibSM64;
using MarioMonkeMadness.Behaviours;
using MarioMonkeMadness.Components;
using MarioMonkeMadness.Interaction;
using MarioMonkeMadness.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx.Logging;
using UnityEngine;
using Logger = BepInEx.Logging.Logger;
using System.Collections;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using GorillaLocomotion;

namespace MarioMonkeMadness
{
    [BepInPlugin(Constants.Guid, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;
        public AssetLoader asl;
        public static List<SM64Mario> _marios = new List<SM64Mario>();
        static List<SM64DynamicTerrain> _surfaceObjects = new List<SM64DynamicTerrain>();
        private GameObject Mario;
        public static GameObject CameraFollow;
        private GTZone Zone;
        public static ManualLogSource Log;
        bool PlayedQuitSFX;
        public Plugin()
        {
            Instance = this;
            new MarioEvents();
            new Configuration(this);
            new Harmony(Constants.Guid).PatchAll(typeof(Plugin).Assembly);

            GorillaTagger.OnPlayerSpawned(OnGameInitialized);
            Log = base.Logger;
            if (RefCache.Config.QuitSound.Value == true)
            {
                Application.wantsToQuit += StopQuitToSFX;
            }
        }

        private bool StopQuitToSFX()
        {
            StartCoroutine(DelayQuit());
            return PlayedQuitSFX;
        }

        void DesktopToggleMario()
        {
            if (!XRSettings.isDeviceActive)
            {
                if (_marios.Count > 0) 
                {
                    GTPlayer.Instance.enabled = true;
                    GTPlayer.Instance.disableMovement = false;
                    GTPlayer.Instance.inOverlay = false;
                    GTPlayer.Instance.InReportMenu = false;
                    GorillaTagger.Instance.rigidbody.isKinematic = false;
                    GTPlayer.Instance.TeleportTo(_marios[0].transform.position + Vector3.up * 1, _marios[0].transform.rotation);
                    RemoveMario();
                }
                else
                {
                    Vector3 vec = Camera.main.transform.position + Vector3.up * 0.32f;
                    Collider[] colliders = Physics.OverlapSphere(vec, 1);
                    foreach (var collider in colliders)
                    {
                        if (collider is not MeshCollider) continue;
                        Destroy(collider.gameObject.AddComponent<SM64StaticTerrain>(), 0.5f);
                    }
                    Interop.StaticSurfacesLoad(LibSM64.Utils.GetAllStaticSurfaces());
                    SpawnMario(vec, Camera.main.transform.localRotation.eulerAngles, GTZone.forest);
                }
            }
        }

        private IEnumerator DelayQuit()
        {
            if (!PlayedQuitSFX)
            {
                Interop.PlaySound(SM64Constants.SOUND_MENU_THANK_YOU_PLAYING_MY_GAME);
                if (_marios.Count >= 1)
                {
                    _marios[0].SetAction(SM64Constants.Action.ACT_CREDITS_CUTSCENE);
                    _marios[0].SetAnim(SM64Constants.MarioAnimID.MARIO_ANIM_CREDITS_WAVING);
                }
                NetworkSystem.Instance.ReturnToSinglePlayer();
                yield return new WaitForSecondsRealtime(3);
                PlayedQuitSFX = true;
                Application.Quit();
            }
        }

        public async void OnGameInitialized()
        {
            // Cache a boolean based on whether the user is playing the game on a Steam platform
            RefCache.IsSteam = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";

            // Cache a tuple (ROM state, ROM path) based on any ROM file which can be found in the current directory
            string[] files = Directory.GetFiles(Path.GetDirectoryName(GetType().Assembly.Location), "*.z64");
            Logger.LogInfo(string.Join(", ", files));
            RefCache.RomData = files.Any() ? Tuple.Create(true, files.First()) : Tuple.Create(false, string.Empty);
            
            Interop.GlobalInit(File.ReadAllBytes(files.First()));
            
            // Prepare the asset loader, when initialized loads all assets found within the assetbundle
            await new AssetLoader().Initialize();

            // Spawn the pipes which are used to spawn Mario across different zones in the game
            SpawnPipe(new Vector3(-66.2741f, 20.633f, -80.9425f), 167f, Tuple.Create(GTZone.forest, 1, "tree/TreeBark (1)"));
            SpawnPipe(new Vector3(-17.2362f, 16.8497f, -110.3898f), 35.2f, Tuple.Create(GTZone.mountain, 1, "Geometry/V2_mountainsidesnow"));
            SpawnPipe(new Vector3(-27.6513f, 13.9975f, -107.9862f), 217f, Tuple.Create(GTZone.city, 1, "CosmeticsRoomAnchor/cosmetics room new/cosmeticsroomatlas (combined by EdMeshCombinerSceneProcessor)"));
            SpawnPipe(new Vector3(-7.1295f, 11.9978f, 19.9363f), 90.8f, Tuple.Create(GTZone.beach, 0, "Beach_Main_Geo/B_Fort_5_5_FBX/B_FORT_5_FLOOR"));
            SpawnPipe(new Vector3(125.0817f, -104.3998f, 77.5353f), 180f, Tuple.Create(GTZone.critters, 0, "Critters/Critters_Environment/Landscape/Critters_Landscape/Critters_LandscapeByMaterial002"));

            CameraFollow = FindObjectOfType<GorillaCameraFollow>().gameObject;
        }

        public void Update()
        {
            // Check for if Mario is outside of the particular zone he is designated to be in
            if (Mario && !ZoneManagement.IsInZone(Zone))
            {
                // Remove our current Mario and notify spawn pipes to deactivate their spawn buttons
                RemoveMario();
                RefCache.Events.Trigger_SetButtonState(null, Models.ButtonType.Spawn, false);
            }
            
            foreach (var o in _surfaceObjects)
                o.contextUpdate();

            foreach (var o in _marios)
                o.contextUpdate();

            if (Keyboard.current.mKey.wasPressedThisFrame)
            {
                DesktopToggleMario();
            }
            if (Gamepad.current != null && Gamepad.current.yButton.wasPressedThisFrame)
            {
                DesktopToggleMario();
            }
        }

        public void FixedUpdate()
        {
            foreach (var o in _surfaceObjects)
                o.contextFixedUpdate();

            foreach (var o in _marios)
                o.contextFixedUpdate();
        }
        public void OnDestroy()
        {
            Interop.GlobalTerminate();
        }
        
        // Logic based around the usage of the MarioSpawnPipe and SM64Mario
        #region General Logic

        public void SpawnPipe(Vector3 position, float direction, Tuple<GTZone, int, string> floorObject)
        {
            // Define and prepare our pipe, when initialized a model is created with a set of buttons which can be used by the player
            MarioSpawnPipe pipe = new();
            pipe.Create(position, direction, floorObject);

            // Define a set of events called by the pipe which invoke an action

            pipe.SpawnOn += () =>
            {
                Collider[] colliders = Physics.OverlapSphere(position, 1);
                foreach (var collider in colliders)
                {
                    if (collider is not MeshCollider) continue; 
                    Destroy(collider.gameObject.AddComponent<SM64StaticTerrain>(), 0.5f);
                }
                Interop.StaticSurfacesLoad(LibSM64.Utils.GetAllStaticSurfaces());
                // Create a new Mario at the location of the Pipe
                SpawnMario(position + Vector3.up * 0.32f, Vector3.up * direction, floorObject.Item1);
            };
            pipe.SpawnOff += () =>
            {
                Interop.PlaySound(SM64Constants.SOUND_MARIO_WAAAOOOW);

                // Remove our current Mario 
                RemoveMario();
            };
            pipe.WingOn += () =>
            {
                // Cache the wing cap state as a positive boolean, which later notifies Mario if he should switch to that state when created
                RefCache.IsWingSession = true;
            };
            pipe.WingOff += () =>
            {
                // Cache the wing cap state as a negative boolean, which later is ignored by Mario
                RefCache.IsWingSession = false;
            };
        }

        public void SpawnMario(Vector3 position, Vector3 direction, GTZone zone)
        {
            var mario = new GameObject("Mario");
            mario.transform.position = position;
            mario.transform.eulerAngles = direction;

            var terr = mario.AddComponent<RealtimeTerrainManager>();
            if (XRSettings.isDeviceActive)
            {
                mario.AddComponent<VRInputProvider>();
            }
            else
            {
                var camsys = CameraFollow.AddComponent<MarioCamFollower>();
                if (Gamepad.current != null)
                {
                    mario.AddComponent<ControllerInputProvider>();
                }
                else
                {
                    mario.AddComponent<WASDInputProvider>();
                    camsys.WASD = true;
                }
                CameraFollow.transform.SetParent(null);
                Camera.main.GetComponent<AudioListener>().enabled = false;
                GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().AddComponent<AudioListener>();
                camsys.transformToFollow = mario.transform;
                CameraFollow.transform.localPosition = mario.transform.position + Vector3.up * 2;
                CameraFollow.transform.localRotation = Quaternion.Euler(Vector3.zero);

                GTPlayer.Instance.enabled = false;
                GTPlayer.Instance.disableMovement = true;
                GTPlayer.Instance.inOverlay = true;
                GTPlayer.Instance.InReportMenu = true;
                GorillaTagger.Instance.rigidbody.isKinematic = true;
            }
            mario.AddComponent<MarioGrabHandler>();
            mario.AddComponent<MarioRescueHandler>();

            var Col = Instantiate(GTPlayer.Instance.bodyCollider, mario.transform, worldPositionStays: false);
            Col.transform.localPosition = Vector3.zero;
            var marioWaterDetector = Col.AddComponent<MarioWaterDetector>();
            Col.isTrigger = true;
            
            var mario_handler = mario.AddComponent<SM64Mario>();
            if (mario_handler.spawned)
            {
                RegisterMario(mario_handler);
                mario.AddComponent<MarioSpawnHandler>();
#if DEBUG
                var dbg = mario.AddComponent<MarioDebugWindow>();
                dbg.MyMario = mario_handler;
                dbg.MyMarioWaterDetector = marioWaterDetector;
                dbg.MyRealtime = terr;
#endif
            }
            
            Zone = zone;
        }
        
        public static void RemoveMario()
        {
            if (!XRSettings.isDeviceActive)
            {
                Destroy(MarioCamFollower.Instance);
                CameraFollow.transform.SetParent(Camera.main.transform);
                Camera.main.GetComponent<AudioListener>().enabled = true;
                Destroy(GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().GetComponent<AudioListener>());
                CameraFollow.transform.localPosition = Vector3.zero;
                CameraFollow.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
            foreach (var mario in _marios)
            {
                UnregisterMario(mario); 
                Destroy(mario.gameObject);
            }
        }

        private void RegisterMario(SM64Mario mario)
        {
            if (!_marios.Contains(mario))
                _marios.Add(mario);
        }

        private static void UnregisterMario(SM64Mario mario)
        {
            if (_marios.Contains(mario))
                _marios.Remove(mario);
        }
        
        public void RegisterSurfaceObject(SM64DynamicTerrain surfaceObject)
        {
            if (!_surfaceObjects.Contains(surfaceObject))
                _surfaceObjects.Add(surfaceObject);
        }

        public void UnregisterSurfaceObject(SM64DynamicTerrain surfaceObject)
        {
            if (_surfaceObjects.Contains(surfaceObject))
                _surfaceObjects.Remove(surfaceObject);
        }
        
         Vector3[] GetColliderVertexPositions(BoxCollider col)
        {
            var trans = col.transform;
            var min = (col.center - col.size * 0.5f);
            var max = (col.center + col.size * 0.5f);

            Vector3 savedPos = trans.position;

            var P000 = trans.TransformPoint(new Vector3(min.x, min.y, min.z));
            var P001 = trans.TransformPoint(new Vector3(min.x, min.y, max.z));
            var P010 = trans.TransformPoint(new Vector3(min.x, max.y, min.z));
            var P011 = trans.TransformPoint(new Vector3(min.x, max.y, max.z));
            var P100 = trans.TransformPoint(new Vector3(max.x, min.y, min.z));
            var P101 = trans.TransformPoint(new Vector3(max.x, min.y, max.z));
            var P110 = trans.TransformPoint(new Vector3(max.x, max.y, min.z));
            var P111 = trans.TransformPoint(new Vector3(max.x, max.y, max.z));

            return new Vector3[] { P000, P001, P010, P011, P100, P101, P110, P111 };
            /*
            var vertices = new Vector3[8];
            var thisMatrix = col.transform.localToWorldMatrix;
            var storedRotation = col.transform.rotation;
            col.transform.rotation = Quaternion.identity;

            var extents = col.bounds.extents;
            vertices[0] = thisMatrix.MultiplyPoint3x4(-extents);
            vertices[1] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, -extents.y, extents.z));
            vertices[2] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, -extents.z));
            vertices[3] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, extents.z));
            vertices[4] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, -extents.z));
            vertices[5] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, extents.z));
            vertices[6] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, extents.y, -extents.z));
            vertices[7] = thisMatrix.MultiplyPoint3x4(extents);

            col.transform.rotation = storedRotation;
            return vertices;
            */
        }
        #endregion
    }
}
