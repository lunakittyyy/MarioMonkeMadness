using GorillaExtensions;
using HarmonyLib;
using LibSM64;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GorillaLocomotion;
using static GorillaLocomotion.GTPlayer;
using UnityEngine;

namespace MarioMonkeMadness.Components
{
    public class RealtimeTerrainManager : MonoBehaviour
    {
        private SM64Mario Mario;
        private bool terrainNeedsReload;

        private readonly List<GTPlayer.MaterialData> MaterialCollection = Instance.materialData;
        private readonly float SlipThreshold = Instance.iceThreshold;

        private readonly HashSet<Collider> initializedColliders = new();

        public IEnumerator Start()
        {
            Transform transform = gameObject.transform; // prevent internal call implementation
            transform.localScale = new Vector3(Mathf.Pow(Constants.TriggerLength, 0.28f), Constants.TriggerLength, Mathf.Pow(Constants.TriggerLength, 0.28f));

            Mario = GetComponent<SM64Mario>();
            yield return new WaitForSeconds(0.25f - Mathf.Pow(Time.deltaTime, 2f));

            gameObject.layer = (int)UnityLayer.GorillaBodyCollider;

            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.MovePosition(transform.position.WithY(transform.position.y - Constants.TriggerLength + 1));
            rigidbody.isKinematic = true;

            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.includeLayers = LayerMask.GetMask("GorillaObject", "MirrorOnly", "NoMirror", "Default", "Water");
            collider.excludeLayers = LayerMask.GetMask("GorillaTrigger", "IgnoreRaycast", "GorillaBoundary");
        }

        private void LateUpdate()
        {
            if (terrainNeedsReload)
            {
                Interop.StaticSurfacesLoad(LibSM64.Utils.GetAllStaticSurfaces());
                terrainNeedsReload = false;
            }
        }
        
        private bool IsValidCollider(Collider collider)
        {
            return collider is MeshCollider && !collider.isTrigger && !collider.GetComponent<SM64StaticTerrain>();
        }

        private void AddTerrainComponent(Collider collider)
        {
            var terrain = collider.gameObject.AddComponent<SM64StaticTerrain>();

            if (collider.TryGetComponent(out GorillaSurfaceOverride surface))
            {
                terrain.TerrainType = TerrainType(surface);
                terrain.SurfaceType = SurfaceType(surface);
            }
        }

        private SM64TerrainType TerrainType(GorillaSurfaceOverride surface)
        {
            return MaterialCollection[surface.overrideIndex].matName switch
            {
                "pitground" => SM64TerrainType.Grass,
                "pitgroundwinter" => SM64TerrainType.Snow,
                "BeachSand" => SM64TerrainType.Sand,
                _ => SM64TerrainType.Grass,
            };
        }

        private SM64SurfaceType SurfaceType(GorillaSurfaceOverride surface)
        {
            return MaterialCollection[surface.overrideIndex].slidePercent >= SlipThreshold
                ? SM64SurfaceType.Ice
                : SM64SurfaceType.Default;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (IsValidCollider(other) && initializedColliders.Add(other))
            {
                AddTerrainComponent(other);
                terrainNeedsReload = true;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (initializedColliders.Remove(other))
            {
                if (other.TryGetComponent<SM64StaticTerrain>(out var terrain))
                {
                    Destroy(terrain);
                    terrainNeedsReload = true;
                }
            }
        }

        public void OnDestroy()
        {
            RefCache.TerrainList.Do(terrain => Destroy(terrain));
            RefCache.TerrainList.Clear();
            RefCache.TerrainUpdated = true;
        }
    }
}
