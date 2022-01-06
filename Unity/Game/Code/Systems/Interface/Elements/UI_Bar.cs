using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JamCat.UI 
{
    public class UI_Bar : UI
    {
        // Variables -> Public

        [Header("Configurations")]
        public Image imgBar;
        public float maxWidth;
        public float height;

        [Header("Test")]
        [SerializeField] private float perc;
        

        // Methods -> Override
        protected override void OnAwake() {

        }

        protected override void OnOpen() {
            
        }
        
        protected override void OnClose() {

        }

        protected override void OnUpdate() {

        }

        
        // Methods -> Public
        public void SetPercentage(float perc) {
            this.perc = perc;
            UpdateImgBar();
        }

        private void UpdateImgBar() {
            float newWidth = Remap(perc, 0, 100, 0, maxWidth);
            imgBar.rectTransform.sizeDelta = new Vector2(newWidth, height);
        }

        float Remap (float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}