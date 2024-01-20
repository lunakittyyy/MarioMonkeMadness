using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace MarioMonkeMadness.Tools
{
    public class Configuration
    {
        private readonly ConfigFile Config;

        public ConfigEntry<bool> CustomColour;
        public ConfigEntry<float> MarioScale;

        public Configuration(BaseUnityPlugin plugin)
        {
            RefCache.Config = this;

            Config = plugin.Config;
            CustomColour = Config.Bind("Appearance", "Custom Colour", false, "This entry determines if Mario's colour scheme should match the colour of the player");

            MarioScale = Config.Bind("Gameplay", "Scale", 200f, "This entry determines the scale of Mario, and is limited from 150 to 250");
            MarioScale.Value = Mathf.Clamp(MarioScale.Value, 150, 250);

            RefCache.Scale = MarioScale.Value;
        }
    }
}
