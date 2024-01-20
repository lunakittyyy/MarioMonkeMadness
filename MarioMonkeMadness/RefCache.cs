using LibSM64;
using MarioMonkeMadness.Tools;
using System;
using System.Collections.Generic;

namespace MarioMonkeMadness
{
    public class RefCache
    {
        // Terrain
        public static List<SM64StaticTerrain> TerrainList = new();
        public static bool TerrainUpdated = true;

        // Tools
        public static Configuration Config;
        public static AssetLoader AssetLoader;

        // Gameplay
        public static float Scale = 200;

        // Logic
        public static bool IsSteam;
        public static Tuple<bool, string> RomData;
    }
}
