using BepInEx;
using GorillaExtensions;
using HarmonyLib;
using LibSM64;
using MarioMonkeMadness.Behaviours;
using MarioMonkeMadness.Components;
using MarioMonkeMadness.Tools;
using MarioMonkeMadness.Utilities;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace MarioMonkeMadness
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0"), BepInPlugin(Constants.Guid, Constants.Name, Constants.Version), ModdedGamemode]
    public class Plugin : BaseUnityPlugin
    {
        private MarioSpawnPipe Pipe;

        private SpawnPoint StumpPoint;
        private GameObject Mario;

        public Plugin()
        {
            new Configuration(this);
            new Harmony(Constants.Guid).PatchAll(typeof(Plugin).Assembly);

            Events.GameInitialized += OnGameInitialized;
        }

        public void SpawnMario(Vector3 location)
        {
            if (Mario != null) return; // We already have a Mario

            // Apply static terrain for the interior of Stump; Mario can only properly spawn when valid terrain exists
            ZoneManagement zoneManager = FindObjectOfType<ZoneManagement>();
            GameObject[] zoneObjects = (GameObject[])zoneManager.GetType().GetField("allObjects", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(zoneManager);
            GameObject treeRoom = zoneObjects.First(zoneObject => zoneObject.name == "TreeRoom");
            Destroy(treeRoom.transform.Find(Constants.BaseObjectPath).gameObject.AddComponent<SM64StaticTerrain>(), 0.5f);

            // Create the Mario object and move him to our location, we will be storing his components here
            Mario = new GameObject(string.Format("{0} | Mario", Constants.Name));
            Mario.transform.position = location;

            // Create the input provider for Mario so we can control him
            MarioInputProvider inputProvider = Mario.AddComponent<MarioInputProvider>();
            inputProvider.cameraObject = GorillaTagger.Instance.mainCamera;

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
            //Destroy(Mario);
            Mario = null;
        }

        public async void OnGameInitialized(object sender, EventArgs e)
        {
            // Load the resources used for the mod
            await AssetUtils.LoadAsset<Shader>("VertexColourShader");
            await AssetUtils.LoadAsset<Shader>("MarioSurfaceShader");
            await AssetUtils.LoadAsset<AudioClip>("Spawn");
            await AssetUtils.LoadAsset<AudioClip>("Despawn");
            await AssetUtils.LoadAsset<GameObject>("MarioSpawner");

            // Define the spawn point which represents the location of the Stump
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            StumpPoint = spawnManager.GetComponentsInChildren<SpawnPoint>().First();
            SpawnPipe();
        }

        public void SpawnPipe()
        {
            // Define the location when the pipe is spawned
            Vector3 pipePosition = (StumpPoint.transform.position + StumpPoint.transform.forward * 2.8f).WithY(12.8822f);

            // Create our pipe which will be used to spawn and despawn Mario
            Pipe = new MarioSpawnPipe();
            Pipe.Create(pipePosition);

            // Define events for our pipe for when its toggled
            Pipe.On += delegate ()
            {
                AudioSource.PlayClipAtPoint(AssetUtils.GetAsset<AudioClip>("Spawn"), pipePosition, 0.4f);
                SpawnMario(pipePosition - Vector3.up * 0.3f); // Spawn Mario just underneath the location of the pipe
            };
            Pipe.Off += delegate ()
            {
                AudioSource.PlayClipAtPoint(AssetUtils.GetAsset<AudioClip>("Despawn"), Mario.transform.position, 0.2f);
                RemoveMario();
            };
        }
    }
}
