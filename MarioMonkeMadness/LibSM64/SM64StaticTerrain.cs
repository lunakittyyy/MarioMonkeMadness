using UnityEngine;

namespace LibSM64
{
    public class SM64StaticTerrain : MonoBehaviour
    {
        [SerializeField] public SM64TerrainType TerrainType = SM64TerrainType.Grass;
        [SerializeField] public SM64SurfaceType SurfaceType = SM64SurfaceType.Default;
    }
}