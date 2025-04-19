using System.Linq;
using GorillaLocomotion;
using LibSM64;
using UnityEngine;

namespace MarioMonkeMadness.Behaviours
{
    public class MarioGrabHandler : MonoBehaviour
    {
        const float Distance = 1f;
        bool isGripping = false;
        bool wasGrippingLastFrame = false;
        SM64Mario MyMario;

        public void Start()
        {
            MyMario = GetComponent<SM64Mario>();
        }
        
        public void LateUpdate()
        {
            if (Vector3.Distance(GTPlayer.Instance.rightControllerTransform.position, transform.position) >
                         Distance) return;
            bool gripButton = ControllerInputPoller.instance.rightGrab;

            if (gripButton && !wasGrippingLastFrame)
            {
                MyMario.SetAction(SM64Constants.Action.ACT_GRABBED);
                Interop.PlaySound(SM64Constants.SOUND_MARIO_WHOA);
                isGripping = true;
            }

            if (gripButton && isGripping)
            {
                //TODO: make mario rotate the same way as player or hand so throw works
                MyMario.SetPosition(GTPlayer.Instance.rightControllerTransform.position += Vector3.down * 0.15f);
            }

            if (!gripButton && wasGrippingLastFrame)
            {
                var tracker = GTPlayer.Instance.rightHandCenterVelocityTracker;
                var vel = tracker.GetLatestVelocity() * 50f;
                MyMario.SetAction(SM64Constants.Action.ACT_THROWN_FORWARD);
                MyMario.SetVelocity(-vel); //For some reason this UP/Down only, need to set Both:
                MyMario.SetForwardVelocity(-vel.z);
                isGripping = false;
            }
            
            wasGrippingLastFrame = gripButton;
        }
    }
}