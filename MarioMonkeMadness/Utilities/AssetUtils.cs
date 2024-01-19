using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace MarioMonkeMadness.Utilities
{
    public static class AssetUtils
    {
        private static AssetBundle Bundle;
        private static bool Initialized;

        private static Task ActiveTask;
        private static readonly Dictionary<string, Object> Cache = new();

        private static async Task LoadBundle()
        {
            var taskCompletionSource = new TaskCompletionSource<AssetBundle>();

            Stream str = typeof(Plugin).Assembly.GetManifestResourceStream(Constants.BundlePath);
            var request = AssetBundle.LoadFromStreamAsync(str);

            request.completed += operation =>
            {
                var outRequest = operation as AssetBundleCreateRequest;
                taskCompletionSource.SetResult(outRequest.assetBundle);
            };

            Bundle = await taskCompletionSource.Task;
            Initialized = true;
        }

        public static async Task LoadAsset<T>(string name) where T : Object
        {
            if (Cache.TryGetValue(name, out var _loadedObject)) return;

            if (!Initialized)
            {
                ActiveTask ??= LoadBundle();
                await ActiveTask;
            }

            var taskCompletionSource = new TaskCompletionSource<T>();
            var request = Bundle.LoadAssetAsync<T>(name);

            request.completed += operation =>
            {
                var outRequest = operation as AssetBundleRequest;
                if (outRequest.asset == null)
                {
                    taskCompletionSource.SetResult(null);
                    return;
                }

                taskCompletionSource.SetResult(outRequest.asset as T);
            };

            var _finishedTask = await taskCompletionSource.Task;
            Cache.Add(name, _finishedTask);
        }

        public static T GetAsset<T>(string name) where T : Object
        {
            if (Cache.TryGetValue(name, out var _loadedObject)) return _loadedObject as T;
            return null;
        }
    }
}
