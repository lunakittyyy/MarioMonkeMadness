using System;
using LibSM64;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MarioMonkeMadness.Behaviours
{
    class WASDInputProvider : SM64InputProvider
    {
        float ws;
        float ad;
        public override Vector3 GetCameraLookDirection()
        {
            return GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().transform.forward;
        }

        public override Vector2 GetJoystickAxes()
        {
            if (Keyboard.current.wKey.isPressed)
            {
                ws = 1;
            }
            else if (Keyboard.current.sKey.isPressed)
            {
                ws = -1;
            }
            else
            {
                ws = 0;
            }

            if (Keyboard.current.aKey.isPressed)
            {
                ad = -1;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                ad = 1;
            }
            else
            {
                ad = 0;
            }

            return new Vector2(ad, ws);
        }

        public override bool GetButtonHeld(Button button) => button switch
        {
            Button.Jump => Keyboard.current.spaceKey.isPressed,
            Button.Kick => Keyboard.current.shiftKey.isPressed,
            Button.Stomp => Keyboard.current.ctrlKey.isPressed,
            _ => throw new IndexOutOfRangeException()
        };
    }
}