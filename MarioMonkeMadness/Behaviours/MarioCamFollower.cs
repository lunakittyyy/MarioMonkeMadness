using System;
using LibSM64;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Valve.VR;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;

namespace MarioMonkeMadness.Behaviours
{
    class MarioCamFollower : MonoBehaviour
    {

        public static MarioCamFollower Instance;
        MarioCamFollower() => Instance = this;

        public Transform transformToFollow;

        public bool Freecam, ReturnState;

        SM64InputProvider inputProvider;
        public enum RotationAxes
        {
            MouseXAndY = 0,
            MouseX = 1,
            MouseY = 2
        }

        public Plugin.ControlType controlType;

        public RotationAxes axes = RotationAxes.MouseXAndY;
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        public float minimumY = -80F;
        public float maximumY = 80F;

        public bool invertY;

        private float rotationY;

        private float GetMouseX() => Mouse.current.delta.ReadValue().x / 400;

        private float GetMouseY() => Mouse.current.delta.ReadValue().y / 400;

        void Start()
        {
            inputProvider = Plugin._mario.GetComponent<SM64InputProvider>();
            if (inputProvider.GetType() == typeof(WASDInputProvider))
            {
                controlType = Plugin.ControlType.WASD;
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (inputProvider.GetType() == typeof(ControllerInputProvider))
            {
                controlType = Plugin.ControlType.Controller;
            }
            if (inputProvider.GetType() == typeof(VRInputProvider))
            {
                controlType = Plugin.ControlType.VR;
            }
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        void OnDestroy()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //TODO: fix noclip again
        float UpOrDown()
        {
            if (controlType == Plugin.ControlType.Controller)
            {
                if (Gamepad.current.aButton.isPressed)
                {
                    return 1;
                }
                if (Gamepad.current.bButton.isPressed)
                {
                    return -1;
                }
                return 0;
            }
            if (controlType == Plugin.ControlType.WASD)
            {
                if (Keyboard.current.spaceKey.isPressed)
                {
                    return 1;
                }
                else if (Keyboard.current.spaceKey.isPressed)
                {
                    return -1;
                }
                return 0;
            }
            return 0;
        }

        private void FixedUpdate()
        {
            switch (controlType)
            {
                case Plugin.ControlType.WASD:
                    HandleWASD();
                    break;
                case Plugin.ControlType.Controller:
                    HandleController();
                    break;
                case Plugin.ControlType.VR:
                    HandleVR();
                    break;
            }
            if (Freecam)
            {
                Plugin._mario.SetPosition(transform.position - Vector3.up * 1);
                Plugin._mario.SetAction(SM64Constants.Action.ACT_DEBUG_FREE_MOVE);
                ReturnState = true;
                Vector2 move = inputProvider.GetJoystickAxes();
                Vector3 vector = new Vector3(move.x, UpOrDown(), move.y);
                transform.position += vector;
            }
            else
            {
                transform.position = transformToFollow.position + Vector3.up * 1;
                if (ReturnState)
                {
                    Plugin._mario.SetAction(SM64Constants.Action.ACT_SPAWN_SPIN_AIRBORNE);
                    ReturnState = false;
                }
            }
        }

        private void HandleVR() => transform.rotation = Camera.main.transform.rotation;

        private void HandleController()
        {
            if (Gamepad.current.rightStickButton.wasPressedThisFrame)
            {
                Freecam = !Freecam;
            }
            Vector3 rot = new Vector3(0, Gamepad.current.rightStick.value.x, 0);
            transform.Rotate(rot, 1);
            if (Freecam)
            {
                Vector3 pos = new Vector3(Gamepad.current.leftStick.value.x, UpOrDown(), Gamepad.current.leftStick.value.y);
                transform.position += pos;
            }
        }

        void HandleWASD()
        {
            if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                Freecam = !Freecam;
            }

            float ySens = sensitivityY;
            if (invertY) { ySens *= -1f; }

            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + GetMouseX() * sensitivityX;

                rotationY += GetMouseY() * ySens;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, GetMouseX() * sensitivityX, 0);
            }
            else
            {
                rotationY += GetMouseY() * ySens;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
