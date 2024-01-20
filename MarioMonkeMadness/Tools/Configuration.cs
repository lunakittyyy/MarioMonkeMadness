using BepInEx;
using BepInEx.Configuration;

namespace MarioMonkeMadness.Tools
{
    public class Configuration
    {
        private readonly ConfigFile Config;

        public ConfigEntry<bool> CustomColour;

        public Configuration(BaseUnityPlugin plugin)
        {
            RefCache.Config = this;

            Config = plugin.Config;
            CustomColour = Config.Bind("Appearance", "Custom Colour", false, "This entry determines if Mario's colour scheme should match the colour of the player");
        }
    }
}
