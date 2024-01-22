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
        private SpawnPoint StumpPoint;
        private GameObject Mario;

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

            // Define the spawn point which represents the location of the Stump
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            StumpPoint = spawnManager.GetComponentsInChildren<SpawnPoint>().First();

            // Create the pipes used to spawn Mario
            SpawnPipe((StumpPoint.transform.position + StumpPoint.transform.forward * 2.8f).WithY(12.8822f), 0f, Tuple.Create(GTZone.forest, 2, "tree/TreeWood"));
            SpawnPipe(new Vector3(23.596f, 11.32f, 5.1617f), 230f, Tuple.Create(GTZone.beach, 0, "Beach_Main_Geo/B_DocksPier_1/B_Dock_Main1"));
        }

        // Logic based around the usage of the MarioSpawnPipe and SM64Mario
        #region General Logic

        public void SpawnPipe(Vector3 position, float direction, Tuple<GTZone, int, string> floorObject)
        {
            // Create our pipe which will be used to spawn and despawn Mario
            MarioSpawnPipe pipe = new();
            pipe.Create(position, direction);

            // Define events for our pipe for when its toggled
            pipe.On += delegate ()
            {
                // Apply static terrain for the interior of Stump; Mario can only properly spawn when valid terrain exists
                ZoneManagement zoneManager = FindObjectOfType<ZoneManagement>();
                ZoneData zoneData = (ZoneData)AccessTools.Method(typeof(ZoneManagement), "GetZoneData").Invoke(zoneManager, new object[] { floorObject.Item1 });
                GameObject zoneRoot = zoneData.rootGameObjects[floorObject.Item2];
                Destroy(zoneRoot.transform.Find(floorObject.Item3).gameObject.AddComponent<SM64StaticTerrain>(), 0.5f);

                AudioSource.PlayClipAtPoint(RefCache.AssetLoader.GetAsset<AudioClip>("Spawn"), position, 0.6f);
                SpawnMario(position - Vector3.up * 0.3f, Vector3.up * direction); // Spawn Mario just underneath the location of the pipe
            };

            pipe.Off += delegate ()
            {
                AudioSource.PlayClipAtPoint(RefCache.AssetLoader.GetAsset<AudioClip>("Despawn"), Mario.transform.position, 0.4f);
                RemoveMario();
            };
        }

        public void SpawnMario(Vector3 location, Vector3 direction)
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
