using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamCat.UI 
{
    public class SysUI : Sys
    {
        // Variables -> Static
        public static SysUI instance;
        public static SysUI Get() { return instance; }

        // Variables
        public Window[] windows_loaded;
        public List<Window> windows_opened = new List<Window>();

        private bool skip_open_game;


        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            for (int i = 0; i < windows_loaded.Length; i++)
                windows_loaded[i].AwakeUI();

            // Open Window fade
            Window_Fade fade = Window_Fade.Get();
            CloseAllWindowsInstead(fade);
        }

        protected override void OnStart() {
            // Start Open Game Logic
            StartCoroutine(IE_OpenGame());
        }

        protected override void OnUpdate() {
            for (int i = 0; i < windows_opened.Count; i++)
                windows_opened[i].UpdateUI();

            // Skip Splash Screen
            if (skip_open_game == false)
                if (Input.anyKeyDown == true)
                    SkipOpenGame();
        }


        // Methods -> Public
        public void CloseAllWindows() {
            for (int i = 0; i < windows_opened.Count; i++)
                windows_opened[i].CloseUI();
        }

        public void CloseAllWindowsInstead(Window target) {
            for (int i = 0; i < windows_opened.Count; i++)
                if (windows_opened[i] == target)
                    windows_opened[i].CloseWindow(0.5f, 0);
            target.OpenWindow(0.5f, 0f);
        }

        public void OpenWindow(Window target, bool close_previous) {
            if (close_previous == true){
                ClosePreviousWindow();
                target.OpenUI();
            }else{
                target.OpenUI();
            }
        }

        public void OpenWindow(Window target) {
            target.OpenUI();
        }

        public void ClosePreviousWindow() {
            if (windows_opened.Count > 1)
                windows_opened[windows_opened.Count - 1].CloseUI();
        }


        // IEnumerators
        IEnumerator IE_OpenGame() {

            yield return new WaitForSeconds(0.2f);

            Window_Fade.Get().CloseWindow(0.25f, 0f);
            Window_SplashScreen.Get().OpenWindow(0.25f, 0.25f);

            yield return new WaitForSeconds(3);

            Window_SplashScreen.Get().CloseWindow(0.25f, 0f);
            Window_MainMenu.Get().OpenWindow(0.25f, 0.25f);

            skip_open_game = true;
        }

        void SkipOpenGame() {
            StopAllCoroutines();
            
            // StopCoroutine(IE_OpenGame());
            Window_Fade.Get().CloseWindow(0.25f, 0f);
            Window_SplashScreen.Get().CloseWindow(0.25f, 0f);
            Window_MainMenu.Get().OpenWindow(0.25f, 0f);
            skip_open_game = true;
        }
    }
}