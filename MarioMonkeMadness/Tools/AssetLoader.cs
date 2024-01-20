using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MarioMonkeMadness.Tools
{
    public class AssetLoader
    {
        private AssetBundle Bundle;
        private readonly Dictionary<Object, string> Assets = new();

        public AssetLoader()
        {
            RefCache.AssetLoader = this;
        }

        public async Task Initialize()
        {
            TaskCompletionSource<AssetBundle> bundleCompletionSource = new();

            Stream str = typeof(Plugin).Assembly.GetManifestResourceStream(Constants.BundlePath);
            AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromStreamAsync(str);

            bundleRequest.completed += operation =>
            {
                var outRequest = operation as AssetBundleCreateRequest;
                bundleCompletionSource.SetResult(outRequest.assetBundle);
            };

            Bundle = await bundleCompletionSource.Task;

            TaskCompletionSource<Object[]> assetCompletionSource = new();
            AssetBundleRequest assetRequest = Bundle.LoadAllAssetsAsync();

            assetRequest.completed += operation =>
            {
                var outRequest = operation as AssetBundleRequest;
                if (outRequest.asset == null)
                {
                    assetCompletionSource.SetResult(null);
                    return;
                }

                assetCompletionSource.SetResult(outRequest.allAssets);
            };

            Object[] assets = await assetCompletionSource.Task;
            assets.Do(asset => Assets.Add(asset, asset.name));
        }

        public T GetAsset<T>(string name) where T : Object
        {
            var assetCollection = Assets.Where(asset => asset.Value == name);
            return assetCollection.Any() ? assetCollection.FirstOrDefault(asset => asset.Key.GetType() == typeof(T)).Key as T : null;
        }

    }
}
