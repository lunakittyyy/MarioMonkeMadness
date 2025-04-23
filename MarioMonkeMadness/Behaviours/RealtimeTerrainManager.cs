using GorillaExtensions;
using HarmonyLib;
using LibSM64;
using System.Collections;
using System.Collections.Generic;
using CjLib;
using GorillaLocomotion;
using Unity.Collections;
using Unity.Jobs;
using static GorillaLocomotion.GTPlayer;
using UnityEngine;

namespace MarioMonkeMadness.Components
{
    public class RealtimeTerrainManager : MonoBehaviour
    {
        private SM64Mario Mario;
        private bool terrainNeedsReload;

        private readonly List<GTPlayer.MaterialData> MaterialCollection = Instance.materialData;
#if DEBUG
        public List<SM64StaticTerrain> terrainList = new List<SM64StaticTerrain>();
#endif
        private BoxCollider coll;
        private readonly float SlipThreshold = Instance.iceThreshold;
        private readonly HashSet<Collider> initializedColliders = new();
        public int staticSurfaceFrameTime = 0;
        public HashSet<GameObject> currentHits = new HashSet<GameObject>();
        public HashSet<GameObject> previousHits = new HashSet<GameObject>();
        private static int gridWidth = 3;
        private static int gridHeight = 3;
        private static float spacing = 0.25f;
        private static float totalWidth = (gridWidth - 1) * spacing;
        private static float totalHeight = (gridHeight - 1) * spacing;
        private LayerMask includedLayers = LayerMask.GetMask("GorillaObject", "Gorilla Object", "MirrorOnly", "NoMirror", "Default");
        private LayerMask excludedLayers = LayerMask.GetMask("GorillaTrigger", "Gorilla Trigger", "IgnoreRaycast", "GorillaBoundary");
        public IEnumerator Start()
        {
            Transform transform = gameObject.transform; // prevent internal call implementation
            transform.localScale = new Vector3(1, 1, 1);

            Mario = GetComponent<SM64Mario>();
            yield return new WaitForSeconds(0.25f - Mathf.Pow(Time.deltaTime, 2f));

            gameObject.layer = (int)UnityLayer.GorillaBodyCollider;

            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.MovePosition(transform.position.WithY(transform.position.y - 150 + 1));
            rigidbody.isKinematic = true;

            coll = gameObject.AddComponent<BoxCollider>();
            coll.isTrigger = true;
            coll.includeLayers = includedLayers;
            coll.excludeLayers = excludedLayers;
        }

        // Welcome to multithreaded math hell. We're shaving off every CPU cycle we can here.
        private void LateUpdate()
        {
            (previousHits, currentHits) = (currentHits, previousHits);
            currentHits.Clear();

            int totalRays = gridWidth * gridHeight;

            var raycastCommands = new NativeArray<RaycastCommand>(totalRays, Allocator.TempJob);
            var raycastResults = new NativeArray<RaycastHit>(totalRays, Allocator.TempJob);

            Vector3 startPosition = transform.position + Vector3.up * 0.35f;
            
            int index = 0;
            for (int x = 0; x < gridWidth; x++)
            {
                for (int z = 0; z < gridHeight; z++)
                {
                    Vector3 rayOrigin = startPosition + new Vector3(x * spacing - totalWidth / 2f, 0, z * spacing - totalHeight / 2f);
                    raycastCommands[index++] = new RaycastCommand(rayOrigin, Vector3.down, 5000f, includedLayers);
                }
            }
            
            JobHandle handle = RaycastCommand.ScheduleBatch(raycastCommands, raycastResults, 64, default);
            handle.Complete();
            
            for (int i = 0; i < totalRays; i++)
            {
                RaycastHit hit = raycastResults[i];
                if (hit.collider)
                {
    #if DEBUG
                    DebugUtil.DrawLine(raycastCommands[i].from, hit.point, Color.red);
                    DebugUtil.DrawBox(coll.bounds.center, Quaternion.identity, new Vector3(1, 1, 1), Color.red);
    #endif
                    currentHits.Add(hit.collider.gameObject);

                    if (!hit.collider.gameObject.TryGetComponent<SM64StaticTerrain>(out _))
                    {
                        ColliderEnter(hit.collider, true);
                    }
                }
            }

            foreach (var obj in previousHits)
            {
                if (!currentHits.Contains(obj))
                {
                    SM64StaticTerrain terr = obj.GetComponent<SM64StaticTerrain>();
                    if (terr)
                    {
                        RemoveTerrainComponent(terr);
                    }
                }
            }
            
            staticSurfaceFrameTime++;
            if (terrainNeedsReload /* && staticSurfaceFrameTime % 5 == 0 */)
            {
            
                Interop.StaticSurfacesLoad(LibSM64.Utils.GetAllStaticSurfaces());
                terrainNeedsReload = false;
                staticSurfaceFrameTime = 0;
            }
            raycastCommands.Dispose();
            raycastResults.Dispose();
        }
        
        private bool IsValidCollider(Collider collider)
        {
            return collider is MeshCollider && !collider.isTrigger && !collider.GetComponent<SM64StaticTerrain>();
        }

        private void AddTerrainComponent(Collider collider, bool wasRaycast)
        {
            var terrain = collider.gameObject.AddComponent<SM64StaticTerrain>();
#if DEBUG
            terrainList.Add(terrain);
#endif
            if (collider.TryGetComponent(out GorillaSurfaceOverride surface))
            {
                terrain.TerrainType = TerrainType(surface);
                terrain.SurfaceType = SurfaceType(surface);
                terrain.wasAddedByRaycast = wasRaycast;
            }
        }

        private SM64TerrainType TerrainType(GorillaSurfaceOverride surface)
        {
            return MaterialCollection[surface.overrideIndex].matName switch
            {
                "pitground" => SM64TerrainType.Grass,
                "pitgroundwinter" => SM64TerrainType.Snow,
                "BeachSand" => SM64TerrainType.Sand,
                "default" => SM64TerrainType.Stone,
                "loglowres" => SM64TerrainType.Spooky,
                "MetroConcrete" => SM64TerrainType.Stone,
                _ => SM64TerrainType.Grass,
            };
        }

        private SM64SurfaceType SurfaceType(GorillaSurfaceOverride surface)
        {
            return MaterialCollection[surface.overrideIndex].slidePercent >= SlipThreshold
                ? SM64SurfaceType.Ice
                : SM64SurfaceType.Default;
        }

        public void ColliderEnter(Collider other, bool wasRaycast = false)
        {
            if (other is MeshCollider && !other.isTrigger && !other.GetComponent<SM64StaticTerrain>())
            {
                AddTerrainComponent(other, wasRaycast);
                terrainNeedsReload = true;
            }
        }


        public void ColliderExit(Collider other)
        {
            if (other.TryGetComponent<SM64StaticTerrain>(out var terrain))
            {
                RemoveTerrainComponent(terrain);
                initializedColliders.Remove(other);
            }
        }

        public void RemoveTerrainComponent(SM64StaticTerrain terrain)
        {
#if DEBUG
            terrainList.Remove(terrain);
#endif
            Destroy(terrain);
            terrainNeedsReload = true;
        }
        
        public void OnTriggerEnter(Collider other) => ColliderEnter(other);

        public void OnTriggerExit(Collider other) => ColliderExit(other);

        public void OnDestroy()
        {
            RefCache.TerrainList.Do(Destroy);
            RefCache.TerrainList.Clear();
            RefCache.TerrainUpdated = true;
        }
    }
}
