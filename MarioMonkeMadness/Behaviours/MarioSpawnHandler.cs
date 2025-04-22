using UnityEngine;
using LibSM64;
using System.Collections;
using Mario = LibSM64.SM64Mario;
using Action = SM64Constants.Action;

namespace MarioMonkeMadness.Behaviours
{
    [DisallowMultipleComponent, RequireComponent(typeof(Mario))]
    public class MarioSpawnHandler : MonoBehaviour
    {
        private Mario Mario => GetComponent<Mario>();

        public void Awake()
        {
            StartCoroutine(nameof(Init));
        }

        public IEnumerator Init()
        {
            int marioId = Mario.marioId;

            Interop.SetWaterLevel(marioId, -50000);
            Interop.SetGasLevel(marioId, -50000);

            Mario.SetMaterial();
            Mario.SetAction(Action.ACT_JUMP);

            /*
            // Define our health bar
            GameObject healthBar = Instantiate(RefCache.AssetLoader.GetAsset<GameObject>("HealthBar"));
            healthBar.transform.SetParent(transform, false);
            healthBar.AddComponent<MarioHealthBar>().Renderer = renderer;

            // Update scaling of objects
            transform.localScale = new Vector3(Mathf.Pow(MarioMonkeMadness.Constants.TriggerLength, 0.28f), MarioMonkeMadness.Constants.TriggerLength, Mathf.Pow(MarioMonkeMadness.Constants.TriggerLength, 0.28f));
            healthBar.transform.localScale = new Vector3(1f / transform.localScale.x, 1f / transform.localScale.y, 1f / transform.localScale.z) / 1.9f;
            for (int i = 0; i < 4; i++)
            {
                Interop.MarioSetForwardVelocity(marioId, i * 0.021f);
                var velocity = Mario.marioState.velocity;
                Interop.MarioSetVelocity(marioId, new Vector3(velocity[0], 0.21f, velocity[2]));

                yield return new WaitForSeconds(0.04f);
            }
            */


            yield return new WaitForSeconds(1.2f);

            if (RefCache.IsWingSession)
            {
                Interop.MarioInteractCap(marioId, SM64Constants.MARIO_WING_CAP, ushort.MaxValue, false);
            }
        }
    }
}
