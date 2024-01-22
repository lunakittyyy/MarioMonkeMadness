using LibSM64;
using MarioMonkeMadness;
using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class MarioInputProvider : SM64InputProvider
{
    public Transform Camera;

    public override Vector3 GetCameraLookDirection() => Camera.forward;

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