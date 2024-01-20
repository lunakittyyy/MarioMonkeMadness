using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LibSM64
{
    public class SM64Context : MonoBehaviour
    {
        private float UpdateTick, FixedUpdateTick;
        private const float FPS = 30;

        static SM64Context s_instance = null;

        List<SM64Mario> _marios = new();
        List<SM64DynamicTerrain> _surfaceObjects = new();

        void Awake()
        {
            Interop.GlobalInit(File.ReadAllBytes(Application.dataPath + "/../baserom.us.z64"));
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

        void OnApplicationQuit()
        {
            Interop.GlobalTerminate();
            s_instance = null;
        }

        static void EnsureInstanceExists()
        {
            if (s_instance == null)
            {
                var contextGo = new GameObject("SM64_CONTEXT");
                contextGo.hideFlags |= HideFlags.HideInHierarchy;
                s_instance = contextGo.AddComponent<SM64Context>();
            }
        }

        static public void RefreshStaticTerrain()
        {
            Interop.StaticSurfacesLoad(Utils.GetAllStaticSurfaces());
        }

        static public void RegisterMario(SM64Mario mario)
        {
            EnsureInstanceExists();

            if (!s_instance._marios.Contains(mario))
                s_instance._marios.Add(mario);
        }

        static public void UnregisterMario(SM64Mario mario)
        {
            if (s_instance != null && s_instance._marios.Contains(mario))
                s_instance._marios.Remove(mario);

            if (s_instance._marios.Count == 0 && s_instance._surfaceObjects.Count == 0)
            {
                Destroy(s_instance.gameObject);
                s_instance = null;
            }
        }

        static public void RegisterSurfaceObject(SM64DynamicTerrain surfaceObject)
        {
            EnsureInstanceExists();

            if (!s_instance._surfaceObjects.Contains(surfaceObject))
                s_instance._surfaceObjects.Add(surfaceObject);
        }

        static public void UnregisterSurfaceObject(SM64DynamicTerrain surfaceObject)
        {
            if (s_instance != null && s_instance._surfaceObjects.Contains(surfaceObject))
                s_instance._surfaceObjects.Remove(surfaceObject);

            if (s_instance._marios.Count == 0 && s_instance._surfaceObjects.Count == 0)
            {
                Destroy(s_instance.gameObject);
                s_instance = null;
            }
        }
    }
}