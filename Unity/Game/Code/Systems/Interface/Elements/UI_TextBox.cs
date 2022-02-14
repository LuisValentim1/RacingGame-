using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JamCat.UI 
{
    public class UI_TextBox : UI
    {
        // Variables
        private CanvasGroup canvasGroup;
        private Text txt;

        // Methods -> Standard
        protected override void OnAwake() {
            canvasGroup = GetComponent<CanvasGroup>();
            txt = GetComponentInChildren<Text>();
        }

        protected override void OnUpdate() {
            UpdateUIPosition();
        }

        protected override void OnOpen() {

        }

        protected override void OnClose() {

        }


        // Methods -> Private
        private void UpdateUIPosition() {
            Vector2 mousePos = Input.mousePosition;
            transform.position = mousePos;
        }

        // Methods -> Public
        public void setVisible(bool value) {
            if (value == true) {
                canvasGroup.alpha = 1;
            } else {
                canvasGroup.alpha = 0;
            }
        }

        public void setText(string value) {
            txt.text = value;
        }
    }
}