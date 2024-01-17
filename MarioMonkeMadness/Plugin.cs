using BepInEx;
using LibSM64;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace MarioMonkeMadness
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static AssetBundle bundle;
        public GameObject mario;
        
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        void Start()
        {

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            // HACK: The surfaces are *required* to have a terrain component or LibSM64 dies to death.
            // Do this, like, actually good
            foreach (GorillaSurfaceOverride obj in FindObjectsByType<GorillaSurfaceOverride>(FindObjectsSortMode.None))
            {
                if (obj.gameObject.TryGetComponent<MeshCollider>(out _))
                {
                    var terrain = obj.gameObject.AddComponent<SM64StaticTerrain>();
                }
            }
            bundle = LoadAssetBundle("MarioMonkeMadness.Resources.marioprefab");
            mario = Instantiate(bundle.LoadAsset<GameObject>("Mario"), GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, Quaternion.identity);
            var InputProv = mario.AddComponent<ExampleInputProvider>();
            InputProv.cameraObject = GorillaTagger.Instance.mainCamera;
            var MarioScript = mario.AddComponent<SM64Mario>();
        }
    }
}
