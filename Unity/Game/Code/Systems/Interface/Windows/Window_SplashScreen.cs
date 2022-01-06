using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace JamCat.UI 
{
    public class Window_SplashScreen : Window
    {
        // Variables -> Instance
        public static Window_SplashScreen instance;
        public static Window_SplashScreen Get() { return instance; }

        // Variables
        public bool skip;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        protected override void OnOpenWindow() {

        }

        protected override void OnCloseWindow() {

        }

        protected override void OnUpdateWindow() {
            
        }

        // Methods -> Public
    }
}