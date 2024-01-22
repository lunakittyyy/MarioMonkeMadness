using MarioMonkeMadness.Interaction;
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

        public void Create(Vector3 position, float direction)
        {
            Pipe = UnityEngine.Object.Instantiate(RefCache.AssetLoader.GetAsset<GameObject>("MarioSpawner"));

            Transform transform = Pipe.transform;
            transform.position = position;
            transform.localScale = Vector3.one * 1.4f;
            transform.localEulerAngles = Vector3.up * direction;

            if (RefCache.RomData.Item1)
            {
                UnityEvent pressEvent = new();
                pressEvent.AddListener(new UnityAction(Press));

                Button = Pipe.transform.Find("Button").gameObject.AddComponent<GorillaPressableButton>();
                Button.buttonRenderer = Button.GetComponent<MeshRenderer>();
                Button.debounceTime = 0.5f;
                Button.myText = Pipe.transform.Find("Button Text").GetComponent<Text>();
                Button.offText = "SPAWN MARIO";
                Button.onText = "DESPAWN MARIO"; // womp womp..
                Button.onPressButton = pressEvent;
            }
            else
            {
                Pipe.GetComponent<Animator>().Play("Warning");
            }

            MarioEvents.SetButtonState += RemoteUpdate;
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

            RefCache.Events.Trigger_SetButtonState(this, Button.isOn);
        }

        public void RemoteUpdate(MarioSpawnPipe sender, bool state)
        {
            if (sender == this) return;

            Button.isOn = state;

            if (Button.isOn)
            {
                Button.myText.text = Button.onText;
                Button.buttonRenderer.material.color = new Color32(116, 116, 116, 255);
            }
            else
            {
                Button.myText.text = Button.offText;
                Button.buttonRenderer.material.color = new Color32(62, 67, 159, 255);
            }
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(Pipe);
            GC.SuppressFinalize(this);
        }
    }
}
