using BepInEx;
using BepInEx.Configuration;
using MarioMonkeMadness.Models;

namespace MarioMonkeMadness.Tools
{
    public class Configuration
    {
        private readonly ConfigFile Config;

        public ConfigEntry<bool> CustomColour, QuitSound, VrDesktopCam;


        public ConfigEntry<ControllerType> JumpButton, KickButton, StompButton, MoveAnalog;

        public Configuration(BaseUnityPlugin plugin)
        {
            RefCache.Config = this;

            Config = plugin.Config;
            CustomColour = Config.Bind("Appearance", "Custom Colour", false, "This entry determines if Mario's colour scheme should match the colour of the player.");

            VrDesktopCam = Config.Bind("Input", "Desktop Camera", true, "This entry determines if The Desktop Camera should be moved when in VR");

            QuitSound = Config.Bind("Sound", "Quit Sound", true, "Cool \"Thanks for playing\" at the end (makes quitting longer but you still leave the room) ");

            JumpButton = Config.Bind("Input", "Jump Controller", ControllerType.RightDevice, "This entry determines which controller is used to make Mario jump.");
            KickButton = Config.Bind("Input", "Kick Controller", ControllerType.RightDevice, "This entry determines which controller is used to make Mario kick.");

            StompButton = Config.Bind("Input", "Stomp Controller", ControllerType.LeftDevice, "This entry determines which controller is used to make Mario stomp.");
            MoveAnalog = Config.Bind("Input", "Movement Controller", ControllerType.LeftDevice, "This entry determines which controller is used to make Mario move.");
        }
    }
}
