using BepInEx;
using LibSM64;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using Utilla;
using MarioMonkeMadness.Components;

namespace MarioMonkeMadness
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static AssetBundle bundle;
        public GameObject mario;
     

        public Plugin()
        {
            new Harmony(PluginInfo.GUID).PatchAll(typeof(Plugin).Assembly);
            Events.GameInitialized += OnGameInitialized;
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
        void OnGameInitialized(object sender, EventArgs e)
        {
            string path = "Environment Objects/LocalObjects_Prefab/TreeRoom/tree/TreeWood";
            GameObject.Find(path).AddComponent<SM64StaticTerrain>();

            // Does not contain Mario's model.
            // This only contains a base Unity material and a shader -- no Nintendo assets are stored within
            bundle = LoadAssetBundle("MarioMonkeMadness.Resources.mariomaterial");

            SpawnPoint stumpPoint = FindObjectOfType<SpawnManager>().GetComponentsInChildren<SpawnPoint>().FirstOrDefault(point => point.startZone == GTZone.forest);
            mario = new GameObject();
            mario.AddComponent<SM64TerrainManager>();
            mario.transform.position = stumpPoint.transform.position;

            var InputProv = mario.AddComponent<ExampleInputProvider>();
            InputProv.cameraObject = GorillaTagger.Instance.mainCamera;
            var MarioScript = mario.AddComponent<SM64Mario>();
        }
    }
}
