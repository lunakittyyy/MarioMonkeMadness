using MarioMonkeMadness.Behaviours;
using MarioMonkeMadness.Models;
using System;

namespace MarioMonkeMadness.Interaction
{
    public class MarioEvents
    {
        public static event Action<MarioSpawnPipe, ButtonType, bool> SetButtonState;

        public MarioEvents()
        {
            RefCache.Events = this;
        }

        public virtual void Trigger_SetButtonState(MarioSpawnPipe sender, ButtonType type, bool state)
            => SetButtonState?.Invoke(sender, type, state);
    }
}
