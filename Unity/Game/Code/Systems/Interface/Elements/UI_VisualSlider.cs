using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JamCat.UI {
    public class UI_VisualSlider : UI {
        
        // Variables
        [Header("Configurable")]

        [Header("Test")]
        public float value;

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

        public void OnValueChanged() {
            value = GetComponent<Slider>().value;
        }

        // Methods -> Public
        
        // Methods -> Private
        private void UpdateGraphics() {
            
        }
    }
}