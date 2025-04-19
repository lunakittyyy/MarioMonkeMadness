using GorillaExtensions;
using LibSM64;
using UnityEngine;
using UnityEngine.UI;

namespace MarioMonkeMadness.Behaviours
{
    public class MarioHealthBar : MonoBehaviour
    {
        /*
        public MeshRenderer Renderer;

        private float Health, Fill, Hue;

        private SM64Mario Mario;

        private RectTransform BarRect;
        private Image BarImage;
        private Text BarText;

        public void Start()
        {
            Mario = transform.parent.GetComponent<SM64Mario>();

            BarRect = transform.Find("Canvas/Bar").GetComponent<RectTransform>();
            BarImage = transform.Find("Canvas/Bar").GetComponent<Image>();
            BarText = transform.Find("Canvas/Text").GetComponent<Text>();

            Health = Mario.MarioHealth() / (float)Interop.SM64_MAX_HEALTH;
        }

        public void Update()
        {
            transform.position = Mario.transform.position.WithY((Renderer.bounds.min.y >= 0 ? Renderer.bounds.max.y : Renderer.bounds.min.y) + 0.07f) - Mario.transform.forward * 0.03f;

            Vector3 forward = Camera.main.transform.position - transform.position;
            Vector3 eulerAngles = Quaternion.LookRotation(forward, Vector3.up).eulerAngles.WithZ(0);
            Quaternion quaternion = Quaternion.Euler(eulerAngles);
            transform.rotation = new Quaternion(0f, quaternion.y, 0f, quaternion.w);

            Health = Mathf.Lerp(Health, Mario.MarioHealth() / (float)Interop.SM64_MAX_HEALTH, 14f * Time.deltaTime);
            Fill = Mathf.Lerp(-95f, -5f, Health);
            Hue = 4f / 6f * Health;

            BarRect.offsetMax = BarRect.offsetMax.WithX(Fill);
            BarImage.color = Color.HSVToRGB(Hue, 0.75f, 1f);
            BarText.text = string.Concat(Mathf.FloorToInt(Health * 100 + 0.5f), "%");
        }
        */
    }
}