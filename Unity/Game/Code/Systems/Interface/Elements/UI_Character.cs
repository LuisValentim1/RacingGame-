using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JamCat.UI 
{
    public class UI_Character : UI
    {
        // Variables
        public Sprite[] sprites;
     
        public UI_Bar uiBarMana;
        public UI_Bar_Array uiBarArrayLives;
        public Image imgCharacter;

        // Methods -> Standard
        protected override void OnAwake() {
            
        }

        protected override void OnUpdate() {

        }

        protected override void OnOpen() {

        }

        protected override void OnClose() {

        }


        // Methods -> Public
        public void ChooseCharacterImg(int i) {
            imgCharacter.sprite = sprites[i];
        }
    }
}