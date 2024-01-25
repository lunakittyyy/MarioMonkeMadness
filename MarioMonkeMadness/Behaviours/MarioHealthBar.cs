using GorillaExtensions;
using LibSM64;
using UnityEngine;

namespace MarioMonkeMadness.Behaviours
{
    public class MarioHealthBar : MonoBehaviour
    {
        public MeshRenderer Renderer;

        private new Transform transform;
        private SM64Mario Mario;

        public void Start()
        {
            transform = gameObject.transform;
            Mario = transform.parent.GetComponent<SM64Mario>();
        }

        public void Update()
        {
            transform.position = Mario.transform.position.WithY(Renderer.bounds.max.y + 0.05f);
        }
    }
}
