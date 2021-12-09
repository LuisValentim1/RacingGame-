using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatJam.UI 
{
    public class Window_Credits : Window
    {
        // Variables -> Instance
        public static Window_Credits instance;
        public static Window_Credits Get() { return instance; }

        // Variables

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
        public void Button_Back() {
            UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
            Window_MainMenu window = Window_MainMenu.Get() as Window_MainMenu;
            UI_Methods.SetFade(window, FadeType.fade_in, 0.5f, 0.5f);
        }
    }
}