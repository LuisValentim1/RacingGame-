using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JamCat.UI 
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
                Window_Fade.Get().CloseWindow(1, 0);
                if (Window_Fade.Get().canvasGroup.alpha <= 0)
                    Application.Quit();
            }
        }

        // Methods -> Public
        public void Button_Continue() {
            CloseWindow(0.2f, 0);
            GeneralMethods.PauseGame(false);
        }

        public void Button_Options() {
            CloseWindow(0.2f, 0);
            Window_Options.Get().OpenWindow(0.2f, 0.2f);
            Window_Options.Get().StartOptions(this);
        }

        public void Button_MainMenu() {
            CloseWindow(0.2f, 0);
            Window_HUD.Get().CloseWindow(0.2f, 0);
            Window_MainMenu.Get().OpenWindow(0.2f, 0.2f);

            GeneralMethods.MainMenu();

            if (Data.Get().gameData.localMode == false)
                SysMultiplayer.Get().Disconnect();
        }

        public void Button_Quit() {
            if (Data.Get().gameData.localMode == false)
                SysMultiplayer.Get().Disconnect();
                
            quit = true;
            Application.Quit();
        }
    }
}