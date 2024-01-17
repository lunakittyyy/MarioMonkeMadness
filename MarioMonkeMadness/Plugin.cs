using BepInEx;
using LibSM64;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using Utilla;

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

        ZoneData FindZoneData(ZoneManagement zoneManager, GTZone zone)
            => (ZoneData)AccessTools.Method(typeof(ZoneManagement), "GetZoneData").Invoke(zoneManager, new object[] { zone });

        void OnGameInitialized(object sender, EventArgs e)
        {
            ZoneManagement zoneManager = FindObjectOfType<ZoneManagement>();
            ZoneData forestData = FindZoneData(zoneManager, GTZone.forest);

            var materialCollection = GorillaLocomotion.Player.Instance.materialData;
            var slipThreshold = GorillaLocomotion.Player.Instance.iceThreshold;

            GameObject stump = forestData.rootGameObjects[1], forest = forestData.rootGameObjects[2];

            foreach(MeshCollider collider in stump.GetComponentsInChildren<MeshCollider>())
            {
                SM64StaticTerrain terrain = collider.gameObject.AddComponent<SM64StaticTerrain>();
                if (collider.TryGetComponent(out GorillaSurfaceOverride surface))
                {
                    terrain.surfaceType = materialCollection[surface.overrideIndex].slidePercent >= slipThreshold ? SM64SurfaceType.Ice : SM64SurfaceType.Default;
                }
            }

            foreach (MeshCollider collider in forest.GetComponentsInChildren<MeshCollider>())
            {
                SM64StaticTerrain terrain = collider.gameObject.AddComponent<SM64StaticTerrain>();
                if (collider.TryGetComponent(out GorillaSurfaceOverride surface))
                {
                    terrain.surfaceType = materialCollection[surface.overrideIndex].slidePercent >= slipThreshold ? SM64SurfaceType.Ice : SM64SurfaceType.Default;
                }
            }

            string path = "Environment Objects/LocalObjects_Prefab/TreeRoom/tree/TreeWood";
            GameObject.Find(path).AddComponent<SM64StaticTerrain>();

            bundle = LoadAssetBundle("MarioMonkeMadness.Resources.marioprefab");

            SpawnPoint stumpPoint = FindObjectOfType<SpawnManager>().GetComponentsInChildren<SpawnPoint>().FirstOrDefault(point => point.startZone == GTZone.forest);
            mario = Instantiate(bundle.LoadAsset<GameObject>("Mario"), stumpPoint.transform.position, Quaternion.identity);

            var InputProv = mario.AddComponent<ExampleInputProvider>();
            InputProv.cameraObject = GorillaTagger.Instance.mainCamera;
            var MarioScript = mario.AddComponent<SM64Mario>();
        }
    }
}
