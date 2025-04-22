using System;
using LibSM64;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MarioMonkeMadness.Behaviours
{
    class ControllerInputProvider : SM64InputProvider
    {
        Camera thirdCam => GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>();
        public override Vector3 GetCameraLookDirection()
        {
            if (!thirdCam) return Vector3.zero;
            return thirdCam.transform.forward;
        }

        public override Vector2 GetJoystickAxes()
        {
            if (Gamepad.current == null) return Vector2.zero;
            return Gamepad.current.leftStick.value;
        }

        public override bool GetButtonHeld(Button button) => button switch
        {
            Button.Jump => Gamepad.current.aButton.isPressed,
            Button.Kick => Gamepad.current.bButton.isPressed,
            Button.Stomp => Gamepad.current.rightTrigger.isPressed,
            _ => throw new IndexOutOfRangeException()
        };
    }
}