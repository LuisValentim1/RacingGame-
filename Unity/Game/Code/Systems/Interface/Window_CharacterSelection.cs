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
        public UI_ToggleGroup toggleGroup;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnUpdateWindow() {

        }

        protected override void OnOpenWindow() {

        }

        protected override void OnCloseWindow() {

        }

        // Methods -> Public
        public void Button_Play() {
            if (Data.gameData.character_selected < 0)
                return;

            UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
            GeneralMethods.StartGame();
            GeneralMethods.PauseGame(false);
        }

        public void Button_Select_Character(int number) {
            Data.gameData.character_selected = number;
        }
        
        public void Button_Back() {
            Data.gameData.character_selected = -1;
            CloseWindow(0.5f, 0);
            Window_MainMenu.Get().OpenWindow(0.5f, 0.5f);
        }
    }
}