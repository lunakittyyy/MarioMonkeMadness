﻿using HarmonyLib;
using MarioMonkeMadness.Interaction;
using MarioMonkeMadness.Models;
using System;
using System.Collections.Generic;
using TMPro;
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
        private readonly Dictionary<ButtonType, Tuple<Color32, Color32>> ButtonColourData = new()
        {
            { ButtonType.Spawn, Tuple.Create(new Color32(116, 116, 116, 255), new Color32(56, 144, 56, 255)) },
            { ButtonType.Wing, Tuple.Create(new Color32(144, 147, 154, 255), new Color32(159, 62, 62, 255)) }
        };

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
            transform.Find("Pipe Model").GetComponent<MeshRenderer>().material.SetFloat(Shader.PropertyToID("_Hue"), Hue(pipeData.Item1));

            if (RefCache.RomData.Item1) // If a Super Mario 64 ROM exists, define and setup our buttons
            {
                UnityEvent spawnEvent = new();
                spawnEvent.AddListener(new UnityAction(() => UpdateButton(ButtonType.Spawn)));

                SpawnButton = Pipe.transform.Find("Selection/Main/Spawn Button").gameObject.AddComponent<GorillaPressableButton>();
                SpawnButton.buttonRenderer = SpawnButton.GetComponent<MeshRenderer>();
                SpawnButton.debounceTime = 0.7f;
                SpawnButton.myTmpText = SpawnButton.transform.Find("Button Text").GetComponent<TextMeshPro>();
                SpawnButton.offText = "Create";
                SpawnButton.onText = "Remove";
                SpawnButton.onPressButton = spawnEvent;

                UnityEvent wingEvent = new();
                wingEvent.AddListener(new UnityAction(() => UpdateButton(ButtonType.Wing)));

                WingButton = Pipe.transform.Find("Selection/Main/Wingcap Button").gameObject.AddComponent<GorillaPressableButton>();
                WingButton.buttonRenderer = WingButton.GetComponent<MeshRenderer>();
                WingButton.debounceTime = 0.25f;
                WingButton.myTmpText = WingButton.transform.Find("Button Text").GetComponent<TextMeshPro>();
                WingButton.offText = "Normal";
                WingButton.onText = "Wing";
                WingButton.onPressButton = wingEvent;
            }
            else // If a Super Mario 64 ROM doesn't exist, display a sign which can futher assist the player with using the ROM
            {
                Pipe.transform.Find("Selection/Main").gameObject.SetActive(false);
                Pipe.transform.Find("Selection/Notice").gameObject.SetActive(true);
            }

            MarioEvents.SetButtonState += RemoteUpdate;
        }

        public void UpdateButton(ButtonType type)
        {
            GorillaPressableButton relativeButton = type == ButtonType.Spawn ? SpawnButton : WingButton;
            relativeButton.isOn ^= true;

            if (relativeButton.isOn) // If the button being pressed is enabled
            {
                relativeButton.myTmpText.text = relativeButton.onText;
                relativeButton.buttonRenderer.material.color = ButtonColourData[type].Item1;

                (type == ButtonType.Spawn ? SpawnOn : WingOn)?.Invoke();
            }
            else // If the button being pressed is disabled
            {
                relativeButton.myTmpText.text = relativeButton.offText;
                relativeButton.buttonRenderer.material.color = ButtonColourData[type].Item2;

                (type == ButtonType.Spawn ? SpawnOff : WingOff)?.Invoke();
            }

            Image buttonImage = relativeButton.myTmpText.transform.Find("Image").GetComponent<Image>();
            buttonImage.sprite = type == ButtonType.Wing ? (relativeButton.isOn ? RefCache.AssetLoader.GetAsset<Sprite>("MarioHat2") : RefCache.AssetLoader.GetAsset<Sprite>("MarioHat")) : buttonImage.sprite;

            RefCache.Events.Trigger_SetButtonState(this, type, relativeButton.isOn);
        }

        public void RemoteUpdate(MarioSpawnPipe sender, ButtonType type, bool state)
        {
            if (sender == this) return;

            GorillaPressableButton relativeButton = type == ButtonType.Spawn ? SpawnButton : WingButton;
            relativeButton.isOn = state;

            if (relativeButton.isOn) // If the button being remotely updated is now enabled
            {
                relativeButton.myTmpText.text = relativeButton.onText;
                relativeButton.buttonRenderer.material.color = ButtonColourData[type].Item1;
            }
            else // If the button being remotely updated is now disabled
            {
                relativeButton.myTmpText.text = relativeButton.offText;
                relativeButton.buttonRenderer.material.color = ButtonColourData[type].Item2;
            }
        }

        public float Hue(GTZone zone) => zone switch
        {
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
