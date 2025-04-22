using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MarioMonkeMadness.Behaviours
{
    class MarioGeoDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent<GorillaGeoHideShowTrigger>(out var hideShowTrigger);
            other.TryGetComponent<GorillaSetZoneTrigger>(out  var setZoneTrigger);
            if (hideShowTrigger != null)
            {
                hideShowTrigger.OnBoxTriggered();
            }
            if (setZoneTrigger != null)
            {
                setZoneTrigger.OnBoxTriggered();
            }
        }
    }
}
