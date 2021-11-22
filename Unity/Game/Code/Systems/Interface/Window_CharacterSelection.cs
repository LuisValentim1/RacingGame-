using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatJam.UI 
{
    public class Window_CharacterSelection : Window
    {
        // Variables -> Instance
        public static Window_CharacterSelection instance;
        public static Window_CharacterSelection Get() { return instance; }

        // Variables
        public int character_selected = -1;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnOpenWindow() {

        }

        protected override void OnCloseWindow() {

        }

        protected override void OnUpdateWindow() {

        }

        // Methods -> Public
        public void Button_Play() {
            if (character_selected < 0)
                return;

            UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
            GeneralMethods.StartGame();
        }

        public void Button_Select_Character(int number) {
            character_selected = number;
        }
        
        public void Button_Back() {
            CloseWindow(0.5f, 0);
            Window_MainMenu.Get().OpenWindow(0.5f, 0.5f);
        }
    }
}