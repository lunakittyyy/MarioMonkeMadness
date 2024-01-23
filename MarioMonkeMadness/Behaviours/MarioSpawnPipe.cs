﻿using HarmonyLib;
using MarioMonkeMadness.Interaction;
using MarioMonkeMadness.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MarioMonkeMadness.Behaviours
{
    public class MarioSpawnPipe : IDisposable
    {
        public event Action SpawnOn, SpawnOff, WingOn, WingOff;

        private GameObject Pipe;

        private GorillaPressableButton SpawnButton, WingButton;
        private readonly Dictionary<ButtonType, Tuple<Color32, Color32>> ButtonColourData = new Dictionary<ButtonType, Tuple<Color32, Color32>>()
        {
            { ButtonType.Spawn, Tuple.Create(new Color32(116, 116, 116, 255), new Color32(62, 67, 159, 255)) },
            { ButtonType.Wing, Tuple.Create(new Color32(144, 147, 154, 255), new Color32(159, 62, 62, 255)) }
        };

        private readonly int HueID = Shader.PropertyToID("_Hue");

        public void Create(Vector3 position, float direction, Tuple<GTZone, int, string> pipeData)
        {
            Pipe = UnityEngine.Object.Instantiate(RefCache.AssetLoader.GetAsset<GameObject>("MarioSpawner"));

            ZoneManagement zoneManager = UnityEngine.Object.FindObjectOfType<ZoneManagement>();
            ZoneData zoneData = (ZoneData)AccessTools.Method(typeof(ZoneManagement), "GetZoneData").Invoke(zoneManager, new object[] { pipeData.Item1 });
            GameObject zoneRoot = zoneData.rootGameObjects[pipeData.Item2];

            Transform transform = Pipe.transform;
            transform.position = position;
            transform.localScale = Vector3.one * 1.4f;
            transform.localEulerAngles = Vector3.up * direction;
            transform.SetParent(zoneRoot.transform, true);
            transform.Find("Pipe Model").GetComponent<MeshRenderer>().material.SetFloat(HueID, Hue(pipeData.Item1));

            if (RefCache.RomData.Item1)
            {
                void UpdateBtn_Spawn() => UpdateButton(ButtonType.Spawn);
                void UpdateBtn_Wing() => UpdateButton(ButtonType.Wing);

                UnityEvent spawnEvent = new();
                spawnEvent.AddListener(new UnityAction(UpdateBtn_Spawn));

                SpawnButton = Pipe.transform.Find("Selection/Spawn Button").gameObject.AddComponent<GorillaPressableButton>();
                SpawnButton.buttonRenderer = SpawnButton.GetComponent<MeshRenderer>();
                SpawnButton.debounceTime = 0.75f;
                SpawnButton.myText = SpawnButton.transform.Find("Button Text").GetComponent<Text>();
                SpawnButton.offText = "SPAWN MARIO";
                SpawnButton.onText = "DESPAWN MARIO";
                SpawnButton.onPressButton = spawnEvent;

                UnityEvent wingEvent = new();
                wingEvent.AddListener(new UnityAction(UpdateBtn_Wing));

                WingButton = Pipe.transform.Find("Selection/Wingcap Button").gameObject.AddComponent<GorillaPressableButton>();
                WingButton.buttonRenderer = WingButton.GetComponent<MeshRenderer>();
                WingButton.debounceTime = 0.75f;
                WingButton.myText = WingButton.transform.Find("Button Text").GetComponent<Text>();
                WingButton.offText = "MARIO CAP";
                WingButton.onText = "WING CAP";
                WingButton.onPressButton = wingEvent;
            }
            else
            {
                Pipe.GetComponent<Animator>().Play("Warning");
            }

            MarioEvents.SetButtonState += RemoteUpdate;
        }

        public void UpdateButton(ButtonType type)
        {
            GorillaPressableButton relativeButton = type == ButtonType.Spawn ? SpawnButton : WingButton;
            relativeButton.isOn ^= true;

            if (relativeButton.isOn)
            {
                relativeButton.myText.text = relativeButton.onText;
                relativeButton.buttonRenderer.material.color = ButtonColourData[type].Item1;

                (type == ButtonType.Spawn ? SpawnOn : WingOn)?.Invoke();
            }
            else
            {
                relativeButton.myText.text = relativeButton.offText;
                relativeButton.buttonRenderer.material.color = ButtonColourData[type].Item2;

                (type == ButtonType.Spawn ? SpawnOff : WingOff)?.Invoke();
            }

            RefCache.Events.Trigger_SetButtonState(this, type, relativeButton.isOn);
        }

        public void RemoteUpdate(MarioSpawnPipe sender, ButtonType type, bool state)
        {
            if (sender == this) return;

            GorillaPressableButton relativeButton = type == ButtonType.Spawn ? SpawnButton : WingButton;
            relativeButton.isOn = state;

            if (relativeButton.isOn)
            {
                relativeButton.myText.text = relativeButton.onText;
                relativeButton.buttonRenderer.material.color = ButtonColourData[type].Item1;
            }
            else
            {
                relativeButton.myText.text = relativeButton.offText;
                relativeButton.buttonRenderer.material.color = ButtonColourData[type].Item2;
            }
        }

        public float Hue(GTZone zone) => zone switch
        {
            GTZone.forest => 0f,
            GTZone.mountain => 4.8f,
            GTZone.beach => 12f,
            GTZone.city => 9.8f,
            _ => 0f
        };

        public void Dispose()
        {
            UnityEngine.Object.Destroy(Pipe);
            GC.SuppressFinalize(this);
        }
    }
}
