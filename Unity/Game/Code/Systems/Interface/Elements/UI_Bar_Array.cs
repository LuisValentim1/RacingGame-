using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JamCat.UI 
{
    public class UI_Bar_Array : UI
    {
        // Variables -> Public

        [Header("Configurations")]
        public Image[] images;

        [Header("Test")]
        [SerializeField] private float value;
        

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
        public void setValue(float value) {
            this.value = value;
            UpdateImgBar();
        }

        private void UpdateImgBar() {
            for (int i = 0; i < images.Length; i++) {
                if (value <= i) {
                    images[i].gameObject.SetActive(false);
                } else {
                    images[i].gameObject.SetActive(true);
                }
            }
        }
    }
}