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
        private bool leftStickClick;
        private SM64Mario MyMario;
        Transform mainCamera;
        bool clickedLastFrame;
        float LastRescueTime;

        void Start()
        {
            MyMario = GetComponent<SM64Mario>();
            mainCamera = Camera.main.transform;
        }

        void Update()
        {
            if (RefCache.IsSteam) leftStickClick = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand);
            else ControllerInputPoller.instance.rightControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftStickClick);

            if (leftStickClick && !clickedLastFrame)
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

            clickedLastFrame = leftStickClick;
        }
    }
}