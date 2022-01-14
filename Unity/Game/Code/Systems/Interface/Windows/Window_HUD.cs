using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace JamCat.UI 
{
    public class Window_HUD : Window
    {
        // Variables -> Instance
        public static Window_HUD instance;
        public static Window_HUD Get() { return instance; }

        // Variables
        public int currenct_character;
        public UI_Bar barMana;
        public UI_Bar barLife;

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
        public void Configure() {
            
        }

        public void Button_PauseMenu() {
            Window_PauseMenu.Get().OpenWindow(0.2f, 0);
        }
    }
}