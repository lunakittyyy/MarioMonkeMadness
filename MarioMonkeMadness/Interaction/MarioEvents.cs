using MarioMonkeMadness.Behaviours;
using System;

namespace MarioMonkeMadness.Interaction
{
    public class MarioEvents
    {
        public static event Action<MarioSpawnPipe, bool> SetButtonState;

        public MarioEvents()
        {
            RefCache.Events = this;
        }

        public virtual void Trigger_SetButtonState(MarioSpawnPipe sender, bool state)
            => SetButtonState?.Invoke(sender, state);
    }
}
