using LibSM64;
using System.Collections.Generic;
using UnityEngine;
using static GorillaLocomotion.Player;

namespace MarioMonkeMadness.Components
{
    internal class SM64TerrainManager : MonoBehaviour
    {
        List<MaterialData> materialCollection = GorillaLocomotion.Player.Instance.materialData;
        float slipThreshold = GorillaLocomotion.Player.Instance.iceThreshold;
        Collider[] hitColliders;
        float colliderHeight = 100;

        void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("Prop");
            var rg = gameObject.AddComponent<Rigidbody>();
            rg.isKinematic = true;
            var col = gameObject.AddComponent<BoxCollider>();
            col.transform.localScale = new Vector3 (3, colliderHeight, 3);
            col.transform.position = new Vector3(col.transform.position.x, (col.transform.position.y - colliderHeight) + 2, col.transform.position.z);
            col.isTrigger = true;
            col.includeLayers = LayerMask.GetMask("Gorilla Object");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.activeSelf && other.gameObject.GetComponent<SM64StaticTerrain>() == null && other is MeshCollider)
            {
                SM64StaticTerrain terrain = other.gameObject.AddComponent<SM64StaticTerrain>();
                if (other.TryGetComponent(out GorillaSurfaceOverride surface))
                {
                    terrain.surfaceType = materialCollection[surface.overrideIndex].slidePercent >= slipThreshold ? SM64SurfaceType.Ice : SM64SurfaceType.Default;
                }
                SM64Context.RefreshStaticTerrain();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.activeSelf && other.gameObject.TryGetComponent<SM64StaticTerrain>(out var terrain))
            {
                Destroy(terrain);
                SM64Context.RefreshStaticTerrain();
            }
        }
    }
}
