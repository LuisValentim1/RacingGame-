using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JamCat.UI 
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
            CloseWindow(0.5f, 0);
            Window_MainMenu.Get().OpenWindow(0.5f, 0.5f);
        }
    }
}