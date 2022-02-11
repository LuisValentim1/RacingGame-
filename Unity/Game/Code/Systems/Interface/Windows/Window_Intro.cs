using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace JamCat.UI 
{
    public class Window_Intro : Window
    {
        // Variables -> Instance
        public static Window_Intro instance;
        public static Window_Intro Get() { return instance; }

        // Variables
        private bool introDone;
        public VideoPlayer videoPlayer;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnOpenWindow() {

        }
        
        protected override void OnCloseWindow() {

        }

        protected override void OnUpdateWindow() {
            if (introDone == true)
                return;

            if (Input.anyKeyDown == true)
                Skip();

            if (videoPlayer.isPlaying == false) {
                if (videoPlayer.time != 0) {
                    Skip();
                }
            }
        }

        public void Skip() {
            videoPlayer.Stop();
            CloseWindow(0.25f, 0.25f);
            Window_SplashScreen.Get().OpenWindow(0.25f, 0f);
            GeneralMethods.MainMenu();
            introDone = true;
        }

        // Methods -> Public
    }
}