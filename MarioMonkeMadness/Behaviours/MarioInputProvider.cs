using GorillaNetworking;
using HarmonyLib;
using LibSM64;
using MarioMonkeMadness;
using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class MarioInputProvider : SM64InputProvider
{
    public GameObject cameraObject = null;

    public override Vector3 GetCameraLookDirection()
    {
        return cameraObject.transform.forward;
    }

    public override Vector2 GetJoystickAxes()
    {
        Vector2 lStick;

        if (RefCache.IsSteam)
            lStick = SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.GetAxis(SteamVR_Input_Sources.LeftHand);
        else
            ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out lStick);

        return lStick;
    }

    public override bool GetButtonHeld(Button button) => button switch
    {
        Button.Jump => ControllerInputPoller.instance.rightControllerPrimaryButton,
        Button.Kick => ControllerInputPoller.instance.rightControllerSecondaryButton,
        Button.Stomp => ControllerInputPoller.instance.leftGrab,
        _ => throw new IndexOutOfRangeException()
    };
}