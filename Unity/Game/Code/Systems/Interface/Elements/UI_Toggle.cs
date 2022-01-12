using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JamCat.UI {
    public class UI_Toggle : UI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
        
        // Variables
        [Header("Configurable")]
        public Image imageBackground;
        public Color colorDisabled;
        public Color colorDefault;
        public Color colorHighlighted;
        public Color colorPressed;

        [Header("Test")]
        public bool isInteractable = true;
        public bool activated;

        private UI_ToggleGroup toggleGroup;

        // Methods -> Standard
        protected override void OnAwake() {
            imageBackground.color = colorDefault;
            toggleGroup = GetComponentInParent<UI_ToggleGroup>();
        }

        protected override void OnUpdate() {
            
        }

        protected override void OnOpen() {

        }

        protected override void OnClose() {

        }

        // Methods -> Public
        public void SetInteractable(bool state) {
            isInteractable = state;
            GetComponent<Button>().interactable = state;

            if (state == false) {
                imageBackground.color = colorDisabled;
            } else {
                imageBackground.color = colorDefault;
            }
        }
        
        public void Toggle() {
            activated = !activated;
            if (activated == true) {
                imageBackground.color = colorPressed;
            } else {
                imageBackground.color = colorDefault;
            }
        }

        public void Activate(bool value) {
            activated = value;
            if (value == true) {
                imageBackground.color = colorPressed;
            } else {
                imageBackground.color = colorDefault;
            }
        }

        // EventSystems
        public void OnPointerEnter(PointerEventData pointerEventData) {
            if (isInteractable == false)
                return;

            if (activated == false)
                imageBackground.color = colorHighlighted;
        }

        public void OnPointerExit(PointerEventData pointerEventData) {
            if (isInteractable == false)
                return;

            if (activated == false)
                imageBackground.color = colorDefault;
        }

        public void OnPointerDown(PointerEventData pointerEventData) {
            if (isInteractable == false)
                return;

            if (toggleGroup != null)
                toggleGroup.ActivateToggle(this);
            else
                Toggle();
        }
    }
}