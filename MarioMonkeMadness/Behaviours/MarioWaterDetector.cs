using System;
using System.Collections.Generic;
using GorillaLocomotion.Swimming;
using LibSM64;
using UnityEngine;

namespace MarioMonkeMadness.Behaviours
{
    class MarioWaterDetector : MonoBehaviour
    {
        public List<WaterVolume> waterVolumes = new List<WaterVolume>();
        public int waterLevel = -50000;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out WaterVolume waterVolume))
            {
                waterVolumes.Add(waterVolume);
                waterLevel = Mathf.RoundToInt(waterVolume.GetComponent<Collider>().bounds.max.y * Interop.SCALE_FACTOR);
                Interop.SetWaterLevel(Plugin._mario.marioId, waterLevel);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out WaterVolume waterVolume))
            {
                waterVolumes.Remove(waterVolume);
                if (waterVolumes.Count > 1)
                {
                    waterLevel = -50000;
                    Interop.SetWaterLevel(Plugin._mario.marioId, waterLevel);
                }
            }
        }
    }
}