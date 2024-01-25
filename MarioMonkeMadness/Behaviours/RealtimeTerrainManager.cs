using GorillaExtensions;
using HarmonyLib;
using LibSM64;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GorillaLocomotion.Player;

namespace MarioMonkeMadness.Components
{
    public class RealtimeTerrainManager : MonoBehaviour
    {
        private SM64Mario Mario;

        private bool Twirling;

        private readonly List<MaterialData> MaterialCollection = Instance.materialData;
        private readonly float SlipThreshold = Instance.iceThreshold;

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

        private SM64SurfaceType SurfaceType(GorillaSurfaceOverride surface) => MaterialCollection[surface.overrideIndex].slidePercent >= SlipThreshold ? SM64SurfaceType.Ice : SM64SurfaceType.Default;

        public void OnTriggerEnter(Collider other)
        {
            if (other is MeshCollider && !other.isTrigger && !other.GetComponent<SM64StaticTerrain>())
            {
                SM64StaticTerrain terrain = other.gameObject.AddComponent<SM64StaticTerrain>();
                if (other.TryGetComponent(out GorillaSurfaceOverride surface))
                {
                    terrain.terrainType = TerrainType(surface);
                    terrain.surfaceType = SurfaceType(surface);
                }
                SM64Context.RefreshStaticTerrain();
            }
        }

        public void OnTriggerStay(Collider other)
        {
            if (other is MeshCollider && !other.isTrigger && !other.GetComponent<SM64StaticTerrain>())
            {
                SM64StaticTerrain terrain = other.gameObject.AddComponent<SM64StaticTerrain>();
                if (other.TryGetComponent(out GorillaSurfaceOverride surface))
                {
                    terrain.terrainType = TerrainType(surface);
                    terrain.surfaceType = SurfaceType(surface);
                }
                SM64Context.RefreshStaticTerrain();
            }
            else if (other.GetComponent<SM64StaticTerrain>() && Physics.OverlapSphere(transform.position - Vector3.up * 0.2f, 0.12f, GetComponent<BoxCollider>().includeLayers, QueryTriggerInteraction.Ignore).Contains(other))
            {
                if (!Twirling && other.TryGetComponent(out GorillaSurfaceOverride surface) && surface.extraVelMultiplier > 1)
                {
                    Mario.SetVelocity(Vector3.up * surface.extraVelMultiplier / 5f);
                    Mario.SetAction(SM64MarioAction.ACT_TWIRLING);
                    Twirling = true;
                }
            }
            else
            {
                Twirling = false;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<SM64StaticTerrain>(out var terrain))
            {
                Destroy(terrain);
                SM64Context.RefreshStaticTerrain();
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
