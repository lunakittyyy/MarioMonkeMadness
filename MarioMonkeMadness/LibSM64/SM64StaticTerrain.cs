using UnityEngine;

namespace LibSM64
{
    public class SM64StaticTerrain : MonoBehaviour
    {
        internal SM64TerrainType terrainType = SM64TerrainType.Grass;
        internal SM64SurfaceType surfaceType = SM64SurfaceType.Default;

        public SM64TerrainType TerrainType { get { return terrainType; }}
        public SM64SurfaceType SurfaceType { get { return surfaceType; }}
    }
}