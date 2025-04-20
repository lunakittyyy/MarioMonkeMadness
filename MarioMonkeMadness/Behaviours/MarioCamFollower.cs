using LibSM64;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MarioMonkeMadness.Behaviours
{
    class MarioCamFollower : MonoBehaviour
    {

        public static MarioCamFollower Instance;
        MarioCamFollower() => Instance = this;

        public Transform transformToFollow;

        public bool WASD, Freecam, ReturnState;

        SM64InputProvider inputProvider;
        public enum RotationAxes
        {
            MouseXAndY = 0,
            MouseX = 1,
            MouseY = 2
        }

        public RotationAxes axes = RotationAxes.MouseXAndY;
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        public float minimumY = -80F;
        public float maximumY = 80F;

        public bool invertY;

        private float rotationY;

        private float GetMouseX() => Mouse.current.delta.ReadValue().x / 400;

        private float GetMouseY() => Mouse.current.delta.ReadValue().y / 400;

        void OnDestroy()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        float UpOrDown()
        {
            if (!WASD)
            {
                if (Gamepad.current.aButton.isPressed)
                {
                    return 1;
                }
                if (Gamepad.current.bButton.isPressed)
                {
                    return -1;
                }
            }
            else
            {
                if (Keyboard.current.spaceKey.isPressed)
                {
                    return 1;
                }
                if (Keyboard.current.ctrlKey.isPressed)
                {
                    return -1;
                }
            }
            return 0;
        }

        private void Update()
        {
            if (!Freecam)
            {
                transform.position = transformToFollow.position + Vector3.up * 1;
                if (ReturnState)
                {
                    Plugin._marios[0].SetAction(SM64Constants.Action.ACT_SPAWN_SPIN_AIRBORNE);
                    ReturnState = false;
                }
            }
            else
            {
                Plugin._marios[0].SetPosition(transform.position - Vector3.up * 1);
                Plugin._marios[0].SetAction(SM64Constants.Action.ACT_DEBUG_FREE_MOVE);
                ReturnState = true;
            }

            if (!WASD)
            {
                if (!inputProvider)
                {
                    inputProvider = FindObjectOfType<ControllerInputProvider>();
                }
                else
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

            }
            else
            {
                if (!inputProvider)
                {
                    inputProvider = FindObjectOfType<WASDInputProvider>();
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    if (Keyboard.current.nKey.wasPressedThisFrame)
                    {
                        Freecam = !Freecam;
                    }

                    if (Freecam)
                    {
                        Vector2 wawa = inputProvider.GetJoystickAxes();
                        Vector3 vector = new Vector3(wawa.x, UpOrDown(), wawa.y);
                        transform.position += vector;
                    }
                    float ySens = sensitivityY;
                    if (invertY) ySens *= -1f;

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
    }
}
