using LibSM64;
using MarioMonkeMadness.Interaction;
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

        // Interaction
        public static MarioEvents Events;

        // Logic
        public static bool IsSteam, IsWingSession;
        public static Tuple<bool, string> RomData;
    }
}
