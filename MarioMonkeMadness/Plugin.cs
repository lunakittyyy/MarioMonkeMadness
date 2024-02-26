using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using LibSM64;
using MarioMonkeMadness.Behaviours;
using MarioMonkeMadness.Components;
using MarioMonkeMadness.Interaction;
using MarioMonkeMadness.Tools;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using Utilla;

namespace MarioMonkeMadness
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0"), BepInPlugin(Constants.Guid, Constants.Name, Constants.Version), ModdedGamemode]
    public class Plugin : BaseUnityPlugin
    {
        public AssetLoader asl;

        private GameObject Mario;
        private GTZone Zone;

        public Plugin()
        {
            new MarioEvents();
            new Configuration(this);
            new Harmony(Constants.Guid).PatchAll(typeof(Plugin).Assembly);

            Events.GameInitialized += OnGameInitialized;
        }

        public async void OnGameInitialized(object sender, EventArgs e)
        {
            // Cache a boolean based on whether the user is playing the game on a Steam platform
            RefCache.IsSteam = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";

            // Cache a tuple (ROM state, ROM path) based on any ROM file which can be found in the current directory
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.z64");
            RefCache.RomData = files.Any() ? Tuple.Create(true, files.First()) : Tuple.Create(false, string.Empty);

            // Prepare the asset loader, when initialized loads all assets found within the assetbundle
            await new AssetLoader().Initialize();

            // Spawn the pipes which are used to spawn Mario across different zones in the game
            SpawnPipe(new Vector3(-66.2741f, 20.633f, -80.9425f), 167f, Tuple.Create(GTZone.forest, 1, "tree/TreeBark (1)"));
            SpawnPipe(new Vector3(-20.8597f, 16.8839f, -102.7351f), 35.2f, Tuple.Create(GTZone.mountain, 1, "Geometry/V2_mountainsidesnow"));
            SpawnPipe(new Vector3(-27.6513f, 13.9975f, -107.9862f), 217f, Tuple.Create(GTZone.city, 1, "CosmeticsRoomAnchor/cosmetics room new/cosmeticsroomatlas (combined by EdMeshCombinerSceneProcessor)"));
            SpawnPipe(new Vector3(-7.1295f, 11.9978f, 19.9363f), 90.8f, Tuple.Create(GTZone.beach, 0, "Beach_Main_Geo/B_Fort_5_5_FBX/B_FORT_5_FLOOR"));
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
                AudioSource.PlayClipAtPoint(RefCache.AssetLoader.GetAsset<AudioClip>("Spawn"), position, 0.6f);

                // Momentarily apply static terrain to the floor underneath the pipe
                ZoneManagement zoneManager = FindObjectOfType<ZoneManagement>();
                ZoneData zoneData = (ZoneData)AccessTools.Method(typeof(ZoneManagement), "GetZoneData").Invoke(zoneManager, new object[] { floorObject.Item1 });
                GameObject zoneRoot = zoneData.rootGameObjects[floorObject.Item2];
                Destroy(zoneRoot.transform.Find(floorObject.Item3).gameObject.AddComponent<SM64StaticTerrain>(), 0.5f);

                // Create a new Mario at the location of the Pipe
                SpawnMario(position + Vector3.up * 0.32f, Vector3.up * direction, floorObject.Item1);
            };
            pipe.SpawnOff += () =>
            {
                AudioSource.PlayClipAtPoint(RefCache.AssetLoader.GetAsset<AudioClip>("Despawn"), Mario.transform.position, 0.4f);

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

        public void SpawnMario(Vector3 location, Vector3 direction, GTZone zone)
        {
            if (Mario != null) return; // We already have a Mario

            // Create the Mario object and move him to our location, we will be storing his components here
            Mario = new GameObject(string.Format("{0} | Mario", Constants.Name));
            Mario.transform.position = location;
            Mario.transform.eulerAngles = direction;

            // Create the input provider for Mario so we can control him
            MarioInputProvider inputProvider = Mario.AddComponent<MarioInputProvider>();
            inputProvider.Camera = GorillaTagger.Instance.mainCamera.transform;

            // Create other components which Mario uses
            Mario.AddComponent<RealtimeTerrainManager>();
            Mario.AddComponent<SM64Mario>();

            // Define the current zone Mario is set in
            Zone = zone;
        }

        public void RemoveMario()
        {
            if (Mario == null) return; // We can't despawn a Mario, there is none

            // Notifies the SM64Mario component to de-register Mario
            Mario.GetComponent<SM64Mario>().enabled = false;

            // Delete the Mario object and fix his reference
            Destroy(Mario);
            Mario = null;
        }

        #endregion
    }
}
