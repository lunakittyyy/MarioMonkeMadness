namespace MarioMonkeMadness
{
    public class Constants
    {
        // General

        /// <summary>
        /// The GUID (globally unique identifier) used to identify the mod
        /// </summary>
        public const string Guid = "luna.mariomonkemadness";

        /// <summary>
        /// The name of the mod
        /// </summary>
        public const string Name = "MarioMonkeMadness";

        /// <summary>
        /// The version of the mod utilizing semantic versioning (major.minor.patch)
        /// </summary>
        public const string Version = "1.0.0.0";

        // Assets

        /// <summary>
        /// The manifest path of the main asset bundle
        /// </summary>
        public const string BundlePath = "MarioMonkeMadness.Resources.mariomaterial";

        // Logic

        /// <summary>
        /// The length of the trigger which generates static terrain on realtime 
        /// </summary>
        public const int TriggerLength = 150;
    }
}
