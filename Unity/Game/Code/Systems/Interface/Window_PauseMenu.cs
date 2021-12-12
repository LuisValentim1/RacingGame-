using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatJam.UI 
{
    public class Window_PauseMenu : Window
    {
        // Variables -> Instance
        public static Window_PauseMenu instance;
        public static Window_PauseMenu Get() { return instance; }

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
        public void Button_Continue() {
            CloseWindow(0.2f, 0);
            GeneralMethods.PauseGame(false);
        }

        public void Button_Options() {
            UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
            Window_Options window = Window_Options.Get() as Window_Options;
            window.StartOptions(this);
            UI_Methods.SetFade(window, FadeType.fade_in, 0.5f, 0.5f);
        }

        public void Button_MainMenu() {
            UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
            Window_MainMenu window = Window_MainMenu.Get() as Window_MainMenu;
            UI_Methods.SetFade(window, FadeType.fade_in, 0.5f, 0.5f);

            GeneralMethods.MainMenu();
        }

        public void Button_Quit() {
            quit = true;
        }
    }
}