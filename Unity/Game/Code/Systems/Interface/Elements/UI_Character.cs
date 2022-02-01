using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JamCat.UI 
{
    public class UI_Character : MonoBehaviour
    {
        public Sprite[] sprites;
     
        public UI_Bar uiBarMana;
        public UI_Bar_Array uiBarArrayLives;
        public Image imgCharacter;

        public void ChooseCharacterImg(int i) {
            imgCharacter.sprite = sprites[i];
        }
    }
}