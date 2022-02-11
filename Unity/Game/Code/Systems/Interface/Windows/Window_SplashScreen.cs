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
        private bool skip;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnOpenWindow() {
            StartCoroutine(IE_SplashScreen());
        }

        protected override void OnCloseWindow() {

        }

        protected override void OnUpdateWindow() {
            if (skip == false)
                if (Input.anyKeyDown == true)
                    Skip();
        }

        // Methods -> Public
        IEnumerator IE_SplashScreen() {

            yield return new WaitForSeconds(3);

            Window_SplashScreen.Get().CloseWindow(0.25f, 0f);
            Window_MainMenu.Get().OpenWindow(0.25f, 0.25f);

            skip = true;
        }

        void Skip() {
            StopAllCoroutines();
            
            // StopCoroutine(IE_OpenGame());
            Window_Fade.Get().CloseWindow(0.25f, 0f);
            Window_SplashScreen.Get().CloseWindow(0.25f, 0f);
            Window_MainMenu.Get().OpenWindow(0.25f, 0f);
            skip = true;
        }
    }
}