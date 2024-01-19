using MarioMonkeMadness.Utilities;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MarioMonkeMadness.Behaviours
{
    public class MarioSpawnPipe : IDisposable
    {
        public event Action On, Off;

        private GameObject Pipe;
        private GorillaPressableButton Button;

        public void Create(Vector3 position)
        {
            Pipe = UnityEngine.Object.Instantiate(AssetUtils.GetAsset<GameObject>("MarioSpawner"));
            Pipe.layer = (int)UnityLayer.Prop;
            Pipe.transform.position = position;
            Pipe.transform.localScale = Vector3.one * 1.4f;

            UnityEvent pressEvent = new();
            pressEvent.AddListener(new UnityAction(Press));

            Button = Pipe.transform.Find("Button").gameObject.AddComponent<GorillaPressableButton>();
            Button.buttonRenderer = Button.GetComponent<MeshRenderer>();
            Button.myText = Pipe.transform.Find("Button Text").GetComponent<Text>();
            Button.offText = "SPAWN MARIO";
            Button.onText = "DESPAWN MARIO"; // womp womp..
            Button.onPressButton = pressEvent;
        }

        public void Press()
        {
            Button.isOn ^= true;

            if (Button.isOn)
            {
                Button.myText.text = Button.onText;
                Button.buttonRenderer.material.color = new Color32(116, 116, 116, 255);

                On?.Invoke();
            }
            else
            {
                Button.myText.text = Button.offText;
                Button.buttonRenderer.material.color = new Color32(62, 67, 159, 255);

                Off?.Invoke();
            }
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(Pipe);
            GC.SuppressFinalize(this);
        }
    }
}
