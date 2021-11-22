using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CatJam.UI 
{
    public class Window_MainMenu : Window
    {
        // Variables -> Instance
        public static Window_MainMenu instance;
        public static Window_MainMenu Get() { return instance; }

        // Variables
        private bool quit;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnOpenWindow() {

        }
        
        protected override void OnCloseWindow() {

        }

        protected override void OnUpdateWindow() {


            if (quit == true) {
                Window_Fade window = Window_Fade.Get() as Window_Fade;
                UI_Methods.SetFade(window, FadeType.fade_out, 1f, 0);
                if (window.canvasGroup.alpha <= 0)
                    Application.Quit();
            }
        }

        // Methods -> Public
        public void Button_Play() {
            CloseWindow(0.2f, 0f);
            Window_CharacterSelection.Get().OpenWindow(0.2f, 0.2f);
        }

        public void Button_Options() {
            CloseWindow(0.2f, 0f);
            Window_Options.Get().OpenWindow(0.2f, 0.2f);
            Window_Options.Get().StartOptions(this);
        }

        public void Button_Credits() {
            CloseWindow(0.2f, 0f);
            Window_Credits.Get().OpenWindow(0.2f, 0.2f);
        }

        public void Button_Quit() {
            quit = true;
        }
    }
}