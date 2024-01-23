using MarioMonkeMadness;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LibSM64
{
    public class SM64Context : MonoBehaviour
    {
        private float UpdateTick, FixedUpdateTick;
        private const float FPS = 30;

        private static SM64Context s_instance = null;
        private static int _staticSurfaceCount = 0;

        private List<SM64Mario> _marios = new();
        private List<SM64DynamicTerrain> _surfaceObjects = new();

        void Awake()
        {
            Interop.GlobalInit(File.ReadAllBytes(RefCache.RomData.Item2)); // RomData is made out of a tuple, Item1 being if a ROM file exists, and Item2 being the path of the ROM
            RefreshStaticTerrain();
        }

        void Update()
        {
            UpdateTick += Time.deltaTime;
            if (UpdateTick > FPS / Mathf.Pow(32, 2))
            {
                UpdateTick = 0;
                foreach (var o in _surfaceObjects)
                    o.contextUpdate();

                foreach (var o in _marios)
                    o.ContextUpdate();
            }
        }

        void FixedUpdate()
        {
            FixedUpdateTick += Time.fixedDeltaTime;
            if (FixedUpdateTick > FPS / Mathf.Pow(32, 2))
            {
                FixedUpdateTick = 0;
                foreach (var o in _surfaceObjects)
                    o.contextFixedUpdate();

                foreach (var o in _marios)
                    o.ContextFixedUpdate();
            }
        }

        private void OnApplicationQuit()
        {
            Terminate();
        }

        private static void ensureInstanceExists()
        {
            if (s_instance == null)
            {
                var contextGo = new GameObject("SM64_CONTEXT");
                contextGo.hideFlags |= HideFlags.HideInHierarchy;
                s_instance = contextGo.AddComponent<SM64Context>();
                DontDestroyOnLoad(contextGo);
            }
        }

        #region Public Methods

        public static void Terminate()
        {
            if (Interop.IsGlobalInit)
            {
                for (int i = 0; i < s_instance._surfaceObjects.Count; i++)
                    if (s_instance._surfaceObjects[i] != null)
                        DestroyImmediate(s_instance._surfaceObjects[i]);
                Interop.GlobalTerminate();
                s_instance = null;
            }
        }

        public static void RefreshStaticTerrain()
        {
            Interop.SM64Surface[] staticSurfaces = Utils.GetAllStaticSurfaces();
            _staticSurfaceCount = staticSurfaces.Length;
            Interop.StaticSurfacesLoad(staticSurfaces);
        }

        public static void RegisterMario(SM64Mario mario)
        {
            ensureInstanceExists();

            if (!s_instance._marios.Contains(mario))
                s_instance._marios.Add(mario);
        }

        public static void UnregisterMario(SM64Mario mario)
        {
            if (s_instance != null && s_instance._marios.Contains(mario))
                s_instance._marios.Remove(mario);

            if (s_instance._marios.Count == 0 && s_instance._surfaceObjects.Count == 0)
            {
                Destroy(s_instance.gameObject);
                s_instance = null;
            }
        }

        public static void RegisterSurfaceObject(SM64DynamicTerrain surfaceObject)
        {
            ensureInstanceExists();

            if (!s_instance._surfaceObjects.Contains(surfaceObject))
                s_instance._surfaceObjects.Add(surfaceObject);
        }

        public static void UnregisterSurfaceObject(SM64DynamicTerrain surfaceObject)
        {
            if (s_instance != null && s_instance._surfaceObjects.Contains(surfaceObject))
                s_instance._surfaceObjects.Remove(surfaceObject);

            if (s_instance._marios.Count == 0 && s_instance._surfaceObjects.Count == 0)
            {
                Destroy(s_instance.gameObject);
                s_instance = null;
            }
        }
        #endregion

        private bool IsActiveInHierarchyAndEnabled(Behaviour o)
        {
            return o.gameObject && o.enabled && o.gameObject.activeInHierarchy;
        }
    }
}