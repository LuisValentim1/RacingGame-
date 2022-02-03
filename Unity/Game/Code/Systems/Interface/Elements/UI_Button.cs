using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JamCat.Audio;

namespace JamCat.UI 
{
    public class UI_Button : UI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        // Variables -> Public
        public bool isInterectable = true;

        [Header("Settings - Background")]
        public Color color_Disabled;
        public Color color_Default;
        public Color color_highlighted;
        public Color color_pressed;
        
        [Header("Settings - Text")]
        public Color color_text_Disabled;
        public Color color_text_Default;
        public Color color_text_highlighted;
        public Color color_text_pressed;

        public AudioClip clipOnClick;
        public AudioClip clipOnHover;


        // ---> Implement Audio

        [Header("Hierarchy")]
        public Image img_background;
        public Text text;

        // Methods -> Override
        protected override void OnAwake() {
            img_background.color = color_Default;
            text.color = color_text_Default;
        }

        protected override void OnOpen() {
            
        }
        
        protected override void OnClose() {

        }


        protected override void OnUpdate() {
            
        }

        public void SetInterectable(bool state) {
            isInterectable = state;
            GetComponent<Button>().interactable = state;

            if (state == false) {
                img_background.color = color_Disabled;
                text.color = color_text_Disabled;
            } else {
                img_background.color = color_Default;
                text.color = color_text_Default;
            }
        }
        
        // Methods -> Event
        public void OnPointerEnter(PointerEventData data) {
            if (isInterectable == false)
                return;

            img_background.color = color_highlighted;
            text.color = color_text_highlighted;
            AudioEffects.Get().PlayAudioEffect(null, clipOnHover);
        }

        public void OnPointerExit(PointerEventData data) {
            if (isInterectable == false)
                return;

            img_background.color = color_Default;
            text.color = color_text_Default;
        }

        public void OnPointerDown(PointerEventData data) {
            if (isInterectable == false)
                return;

            img_background.color = color_pressed;
            text.color = color_text_pressed;
            AudioEffects.Get().PlayAudioEffect(null, clipOnClick);
        }
    }
}