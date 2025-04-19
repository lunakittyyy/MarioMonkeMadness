using GorillaNetworking;
using HarmonyLib;
using LibSM64;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace MarioMonkeMadness.Behaviours
{
    public class MarioRescueHandler : MonoBehaviour
    {
        bool leftStickClickSteam;
        private bool leftStickClickRift;
        private bool IsSteamVR;
        private SM64Mario MyMario;
        Transform mainCamera;
        bool clickedLastFrame;
        float LastRescueTime;

        void Start()
        {
            bool IsSteamVR = PlayFabAuthenticator.instance.platform.ToString().ToLower() == "steam";
            MyMario = GetComponent<SM64Mario>();
            mainCamera = Camera.main.transform;
        }

        void Update()
        {
            // TODO: HACK: My SteamVR detection was being bitchass and it was not correctly identifying an obvious string even after I printed it into the console and checked its value for myself.
            // This is my janky solution for now even though half of this code is unneeded and arbitrary for any given user. I hate 3AM programming. Why are computers like this???? We have these
            // rocks, humps of silicone and metals that we tricked into thinking, that power the entire world, uphold our societies... but it can't check a word correctly? What do I know, I'm just a lowly modder...
            leftStickClickSteam = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand);
            ControllerInputPoller.instance.rightControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftStickClickRift);

            if (leftStickClickSteam || leftStickClickRift && !clickedLastFrame)
            {
                if (LastRescueTime + 3 < Time.time)
                {
                    MyMario.SetAction(SM64Constants.Action.ACT_SPAWN_SPIN_AIRBORNE);
                    MyMario.SetPosition(new Vector3(mainCamera.position.x, mainCamera.position.y + 0.75f,
                        mainCamera.position.z));
                    Interop.PlaySound(SM64Constants.SOUND_ACTION_TELEPORT);
                    LastRescueTime = Time.time;
                }
                else {
                    Interop.PlaySound(SM64Constants.SOUND_MENU_CAMERA_BUZZ);
                }
            }
            clickedLastFrame = leftStickClickSteam || leftStickClickRift;
        }
    }
}