using GorillaExtensions;
using GorillaLocomotion;
using LibSM64;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GorillaLocomotion.Player;

namespace MarioMonkeMadness.Components
{
    public class RealtimeTerrainManager : MonoBehaviour
    {
        private readonly List<MaterialData> MaterialCollection = Instance.materialData;
        private readonly float SlipThreshold = Instance.iceThreshold;

        public IEnumerator Start()
        {
            yield return new WaitForSeconds(0.25f - Time.deltaTime);

            Transform transform = gameObject.transform; // prevent internal call implementation

            gameObject.layer = (int)UnityLayer.Prop;

            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.MovePosition(transform.position.WithY(transform.position.y - Constants.TriggerLength + 1));
            rigidbody.isKinematic = true;

            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            transform.localScale = new Vector3(Mathf.Pow(Constants.TriggerLength, 0.28f), Constants.TriggerLength, Mathf.Pow(Constants.TriggerLength, 0.28f));
            collider.isTrigger = true;
            collider.includeLayers = LayerMask.GetMask(UnityLayer.GorillaObject.ToString(), UnityLayer.MirrorOnly.ToString(), UnityLayer.NoMirror.ToString(), UnityLayer.Default.ToString());
            collider.excludeLayers = LayerMask.GetMask(UnityLayer.GorillaTrigger.ToString(), UnityLayer.IgnoreRaycast.ToString(), UnityLayer.GorillaBoundary.ToString());
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
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<SM64StaticTerrain>(out var terrain))
            {
                Destroy(terrain);
                SM64Context.RefreshStaticTerrain();
            }
        }
    }
}
