using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JamCat.UI {
    public class UI_Toggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
        
        // Variables
        [Header("Configurable")]
        public Image image_background;
        public Color color_default;
        public Color color_highlighted;
        public Color color_pressed;

        [Header("Test")]
        public bool activated;

        private UI_ToggleGroup toggleGroup;

        // Methods -> Standard
        public void AwakeToggle() {
            image_background.color = color_default;
            toggleGroup = GetComponentInParent<UI_ToggleGroup>();
        }

        // Methods -> Public
        public void Activate(bool value) {
            activated = value;
            if (value == true) {
                image_background.color = color_highlighted;
            } else {
                image_background.color = color_default;
            }
        }

        // EventSystems
        public void OnPointerEnter(PointerEventData pointerEventData) {
            if (activated == false)
                image_background.color = color_highlighted;
        }

        public void OnPointerExit(PointerEventData pointerEventData) {
            if (activated == false)
                image_background.color = color_default;
        }

        public void OnPointerDown(PointerEventData pointerEventData) {
            toggleGroup.ActivateToggle(this);
        }
    }
}