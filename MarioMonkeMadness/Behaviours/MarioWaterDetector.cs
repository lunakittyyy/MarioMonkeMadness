using GorillaLocomotion.Swimming;
using LibSM64;
using UnityEngine;

namespace MarioMonkeMadness.Behaviours
{
    class MarioWaterDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<WaterVolume>() != null)
            {
                WaterVolume waterVolume = other.GetComponent<WaterVolume>();
                Interop.SetWaterLevel(Plugin._marios[0].marioId, Mathf.RoundToInt(waterVolume.GetComponent<Collider>().bounds.max.y));
            }
        }
    }
}