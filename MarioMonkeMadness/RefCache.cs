using LibSM64;
using MarioMonkeMadness.Tools;
using System.Collections.Generic;

namespace MarioMonkeMadness
{
    public class RefCache
    {
        public static List<SM64StaticTerrain> TerrainList = new();
        public static bool TerrainUpdated = true;

        public static Configuration Config;
    }
}
