using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JamCat.UI 
{
    public class UI_Button : UI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        // Variables -> Public
        [Header("Settings - Background")]
        public Color color_Default;
        public Color color_highlighted;
        public Color color_pressed;
        
        [Header("Settings - Text")]
        public Color color_text_Default;
        public Color color_text_highlighted;
        public Color color_text_pressed;



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

        
        // Methods -> Event
        public void OnPointerEnter(PointerEventData data) {
            img_background.color = color_highlighted;
            text.color = color_text_highlighted;
        }

        public void OnPointerExit(PointerEventData data) {
            img_background.color = color_Default;
            text.color = color_text_Default;
        }

        public void OnPointerDown(PointerEventData data) {
            img_background.color = color_pressed;
            text.color = color_text_pressed;
        }
    }
}