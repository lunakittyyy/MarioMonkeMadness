using GorillaExtensions;
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

            gameObject.layer = (int)UnityLayer.GorillaBodyCollider;

            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.MovePosition(transform.position.WithY(transform.position.y - Constants.TriggerLength + 1));
            rigidbody.isKinematic = true;

            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            transform.localScale = new Vector3(Mathf.Pow(Constants.TriggerLength, 0.28f), Constants.TriggerLength, Mathf.Pow(Constants.TriggerLength, 0.28f));
            collider.isTrigger = true;
            // collider.includeLayers = LayerMask.GetMask("Gorilla Object", "Default", "NoMirror", "Ignore Raycast");
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other is MeshCollider && !other.isTrigger && !other.GetComponent<SM64StaticTerrain>())
            {
                SM64StaticTerrain terrain = other.gameObject.AddComponent<SM64StaticTerrain>();
                if (other.TryGetComponent(out GorillaSurfaceOverride surface))
                {
                    terrain.surfaceType = MaterialCollection[surface.overrideIndex].slidePercent >= SlipThreshold ? SM64SurfaceType.Ice : SM64SurfaceType.Default;
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
                    terrain.surfaceType = MaterialCollection[surface.overrideIndex].slidePercent >= SlipThreshold ? SM64SurfaceType.Ice : SM64SurfaceType.Default;
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
