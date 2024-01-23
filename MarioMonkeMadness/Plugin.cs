using BepInEx;
using GorillaExtensions;
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
            // Cache whether we are playing on a Steam platform
            RefCache.IsSteam = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";

            // Check our Gorilla Tag directory for any ROMs and store them in the cache
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.z64");
            RefCache.RomData = files.Any() ? Tuple.Create(true, files.First()) : Tuple.Create(false, string.Empty);

            // Prepare the asset loader, which will retrive assets used throughout the mod
            await new AssetLoader().Initialize();

            // Spawn the pipes which are used to spawn Mario across different zones in the game
            SpawnPipe(new Vector3(-66.2741f, 20.633f, -80.9425f), 167f, Tuple.Create(GTZone.forest, 2, "tree/TreeBark (1)"));
            SpawnPipe(new Vector3(-20.8597f, 16.8839f, -102.7351f), 35.2f, Tuple.Create(GTZone.mountain, 1, "Geometry/V2_mountainsidesnow"));
            // SpawnPipe(new Vector3(-88.9367f, 183.2826f, -114.4222f), 268.1f, Tuple.Create(GTZone.skyJungle, 1, "Village (1)/OneHousePlatform (3)"));
            SpawnPipe(new Vector3(-27.6513f, 13.9975f, -107.9862f), 217f, Tuple.Create(GTZone.city, 1, "CosmeticsRoomAnchor/cosmetics room new/cosmeticsroomatlas (combined by EdMeshCombinerSceneProcessor)"));
            SpawnPipe(new Vector3(-7.1295f, 11.9978f, 19.9363f), 90.8f, Tuple.Create(GTZone.beach, 0, "Beach_Main_Geo/B_Fort_5_5_FBX/B_FORT_5_FLOOR"));
        }

        public void Update()
        {
            // Check if both Mario exists and his particular zone isn't active
            if (Mario && !ZoneManagement.IsInZone(Zone))
            {
                RemoveMario();
                RefCache.Events.Trigger_SetButtonState(null, Models.ButtonType.Spawn, false);
            }
        }

        // Logic based around the usage of the MarioSpawnPipe and SM64Mario
        #region General Logic

        public void SpawnPipe(Vector3 position, float direction, Tuple<GTZone, int, string> floorObject)
        {
            // Create our pipe which will be used to spawn and despawn Mario
            MarioSpawnPipe pipe = new();
            pipe.Create(position, direction, floorObject);

            // Define events for our pipe for when its toggled
            pipe.SpawnOn += delegate ()
            {
                // Apply static terrain for the interior of Stump; Mario can only properly spawn when valid terrain exists
                ZoneManagement zoneManager = FindObjectOfType<ZoneManagement>();
                ZoneData zoneData = (ZoneData)AccessTools.Method(typeof(ZoneManagement), "GetZoneData").Invoke(zoneManager, new object[] { floorObject.Item1 });
                GameObject zoneRoot = zoneData.rootGameObjects[floorObject.Item2];
                Destroy(zoneRoot.transform.Find(floorObject.Item3).gameObject.AddComponent<SM64StaticTerrain>(), 0.5f);

                AudioSource.PlayClipAtPoint(RefCache.AssetLoader.GetAsset<AudioClip>("Spawn"), position, 0.6f);
                SpawnMario(position + Vector3.up * 0.32f, Vector3.up * direction, floorObject.Item1);
            };
            pipe.SpawnOff += delegate ()
            {
                AudioSource.PlayClipAtPoint(RefCache.AssetLoader.GetAsset<AudioClip>("Despawn"), Mario.transform.position, 0.4f);
                RemoveMario();
            };
            pipe.WingOn += () =>
            {
                RefCache.IsWingSession = true;
            };
            pipe.WingOff += () =>
            {
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
