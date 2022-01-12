using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JamCat.UI {
    public class UI_Slider : UI {
        
        // Variables
        [Header("Configurable")]
        public Button buttoAdd;
        public Button buttoRemove;
        public Text textValue;

        [Header("Test")]
        public int value;

        // Methods -> Standard
        protected override void OnAwake() {
            value = 1;
        }

        protected override void OnUpdate() {

        }

        protected override void OnOpen() {

        }

        protected override void OnClose() {

        }

        // Methods -> Public
        public void Add() {
            value = Math.Clamp(value + 1, 1, 4);
            UpdateGraphics();
        }

        public void Remove() {
            value = Math.Clamp(value - 1, 1, 4);
            UpdateGraphics();
        }
        
        // Methods -> Private
        private void UpdateGraphics() {
            textValue.text = "" + value;
            
            if (value == 1)
                buttoRemove.interactable = false;
            else
                buttoRemove.interactable = true;

            if (value == 4)
                buttoAdd.interactable = false;
            else
                buttoAdd.interactable = true;
        }
    }
}