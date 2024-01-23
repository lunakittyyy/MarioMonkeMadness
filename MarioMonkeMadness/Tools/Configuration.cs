using BepInEx;
using BepInEx.Configuration;
using MarioMonkeMadness.Models;

namespace MarioMonkeMadness.Tools
{
    public class Configuration
    {
        private readonly ConfigFile Config;

        public ConfigEntry<bool> CustomColour;
        public ConfigEntry<bool> Interpolation;

        public ConfigEntry<ControllerType> JumpButton, KickButton, StompButton, AnalogButton;

        public Configuration(BaseUnityPlugin plugin)
        {
            RefCache.Config = this;

            Config = plugin.Config;
            CustomColour = Config.Bind("Appearance", "Custom Colour", false, "This entry determines if Mario's colour scheme should match the colour of the player.");
            Interpolation = Config.Bind("Appearance", "Interpolation", false, "This entry determines if Mario's animations are interpolated based on frame rate, which may give mixed results.");

            JumpButton = Config.Bind("Input", "Jump Controller", ControllerType.RightDevice, "This entry determines which controller is used to make Mario jump.");
            KickButton = Config.Bind("Input", "Kick Controller", ControllerType.RightDevice, "This entry determines which controller is used to make Mario kick.");
            StompButton = Config.Bind("Input", "Stomp Controller", ControllerType.LeftDevice, "This entry determines which controller is used to make Mario stomp.");
            AnalogButton = Config.Bind("Input", "Movement Controller", ControllerType.LeftDevice, "This entry determines which controller is used to make Mario move.");
        }
    }
}
