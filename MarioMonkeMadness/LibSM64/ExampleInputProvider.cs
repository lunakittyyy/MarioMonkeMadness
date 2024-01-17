using UnityEngine;
using LibSM64;
using GorillaNetworking;
using HarmonyLib;
using Valve.VR;
using UnityEngine.XR;

public class ExampleInputProvider : SM64InputProvider
{
    public GameObject cameraObject = null;

    public override Vector3 GetCameraLookDirection()
    {
        return cameraObject.transform.forward;
    }

    public override Vector2 GetJoystickAxes()
    {
        Vector2 lStick;
        bool IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";
        if (IsSteamVR) { lStick = SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.GetAxis(SteamVR_Input_Sources.LeftHand); }
        else { ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out lStick); }
        return lStick;
    }

    public override bool GetButtonHeld( Button button )
    {
        switch( button )
        {
            case SM64InputProvider.Button.Jump:  return ControllerInputPoller.instance.rightControllerPrimaryButton;
            case SM64InputProvider.Button.Kick:  return ControllerInputPoller.instance.rightControllerSecondaryButton;
            case SM64InputProvider.Button.Stomp: return ControllerInputPoller.instance.leftGrab;
        }
        return false;
    }
}