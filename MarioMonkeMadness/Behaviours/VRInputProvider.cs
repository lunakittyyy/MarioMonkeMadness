using System;
using System.Runtime;
using LibSM64;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using Valve.VR;

namespace MarioMonkeMadness.Behaviours
{
    public class VRInputProvider : SM64InputProvider
    {
        public override Vector3 GetCameraLookDirection()
        {
            return Camera.main.transform.forward;
        }

        public override Vector2 GetJoystickAxes()
        {
            Vector2 joystickAxis;
            if (RefCache.IsSteam)
            {
                SteamVR_Action_Vector2 joystick = RefCache.Config.AnalogButton.Value == 0 ? SteamVR_Actions.gorillaTag_LeftJoystick2DAxis : SteamVR_Actions.gorillaTag_RightJoystick2DAxis;
                joystickAxis = joystick.GetAxis(RefCache.Config.AnalogButton.Value == 0 ? SteamVR_Input_Sources.LeftHand : SteamVR_Input_Sources.RightHand);
            }
            else
            {
                InputDevice device = RefCache.Config.AnalogButton.Value == 0 ? ControllerInputPoller.instance.leftControllerDevice : ControllerInputPoller.instance.rightControllerDevice;
                device.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickAxis);
            }

            return joystickAxis;
        }

        public override bool GetButtonHeld(Button button) => button switch
        {
            Button.Jump => ControllerInputPoller.PrimaryButtonPress(RefCache.Config.JumpButton.Value == 0 ? XRNode.LeftHand : XRNode.RightHand),
            Button.Kick => ControllerInputPoller.SecondaryButtonPress(RefCache.Config.KickButton.Value == 0 ? XRNode.LeftHand : XRNode.RightHand),
            Button.Stomp => ControllerInputPoller.GetGrab(RefCache.Config.StompButton.Value == 0 ? XRNode.LeftHand : XRNode.RightHand),
            _ => throw new IndexOutOfRangeException()
        };
    }
}