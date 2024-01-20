using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace MarioMonkeMadness.Tools
{
    public class Configuration
    {
        private readonly ConfigFile Config;

        public ConfigEntry<bool> CustomColour;
        public ConfigEntry<bool> Interpolation;

        public ConfigEntry<float> MarioScale;

        public Configuration(BaseUnityPlugin plugin)
        {
            RefCache.Config = this;

            Config = plugin.Config;
            CustomColour = Config.Bind("Appearance", "Custom Colour", false, "This entry determines if Mario's colour scheme should match the colour of the player.");
            Interpolation = Config.Bind("Appearance", "Interpolation", false, "This entry determines if Mario's animations are interpolated based on frame rate, which may give mixed results.");
            
            MarioScale = Config.Bind("Gameplay", "Scale", 200f, "This entry determines the scale of Mario, with a smaller number making Mario bigger, and vice versa.");

            RefCache.Scale = MarioScale.Value;
        }
    }
}
