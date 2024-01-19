using MarioMonkeMadness;
using UnityEngine;

namespace LibSM64
{
    public class SM64StaticTerrain : MonoBehaviour
    {
        internal SM64TerrainType terrainType = SM64TerrainType.Grass;
        internal SM64SurfaceType surfaceType = SM64SurfaceType.Default;

        public SM64TerrainType TerrainType { get { return terrainType; } }
        public SM64SurfaceType SurfaceType { get { return surfaceType; } }

        public void Awake()
        {
            RefCache.TerrainList.Add(this);
            RefCache.TerrainUpdated = true;
        }

        public void OnDestroy()
        {
            RefCache.TerrainList.Remove(this);
            RefCache.TerrainUpdated = true;
        }
    }
}