using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JamCat.Audio;

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
        public bool isServerEnabled = true;
        public bool isLocalEnabled = true;
        public bool activated;

        private UI_ToggleGroup toggleGroup;

        public AudioClip clipOnClick;
        public AudioClip clipOnHover;

        // Methods -> Standard
        protected override void OnAwake() {
            imageBackground.color = colorDefault;
            toggleGroup = GetComponentInParent<UI_ToggleGroup>();
        }

        protected override void OnUpdate() {
            
        }

        protected override void OnOpen() {
            activated = false;
            imageBackground.color = colorDefault;
        }

        protected override void OnClose() {

        }

        // Methods -> Public
        public void setServerEnabled(bool state) {
            isServerEnabled = state;
            GetComponent<Button>().interactable = state;
            if (isServerEnabled == true) {
                if (activated == false)
                    imageBackground.color = colorDefault;
                else
                    imageBackground.color = colorPressed;
            } else {
                if (activated == true && toggleGroup != null) {
                    Activate(false);
                    toggleGroup.toggleActivated = null;
                }
                imageBackground.color = colorDisabled;
            }
        }

        public void Toggle() {
            if (isServerEnabled == false)
                return;

            activated = !activated;
            if (activated == true) {
                imageBackground.color = colorPressed;
            } else {
                imageBackground.color = colorDefault;
            }
        }

        public void Activate(bool value) {            
            if (isServerEnabled == false)
                return;
                
            activated = value;
            if (value == true) {
                imageBackground.color = colorPressed;
            } else {
                imageBackground.color = colorDefault;
            }
        }

        // EventSystems
        public void OnPointerEnter(PointerEventData pointerEventData) {
            if (isServerEnabled == false)
                return;

            if (activated == false)
                imageBackground.color = colorHighlighted;

            AudioEffects.Get().Play(clipOnHover);
        }

        public void OnPointerExit(PointerEventData pointerEventData) {
            if (isServerEnabled == false)
                return;

            if (activated == false)
                imageBackground.color = colorDefault;
        }

        public void OnPointerDown(PointerEventData pointerEventData) {
            if (isServerEnabled == false)
                return;

            if (toggleGroup != null)
                toggleGroup.ActivateToggle(this);
            else
                Toggle();
         
            AudioEffects.Get().Play(clipOnClick);
        }
    }
}