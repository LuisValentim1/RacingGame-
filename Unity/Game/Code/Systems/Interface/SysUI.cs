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



        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            for (int i = 0; i < windows_loaded.Length; i++)
                windows_loaded[i].AwakeUI();

            // Open Window fade
            // Window_Fade.Get().canvasGroup.alpha = 1;
            // CloseAllWindowsInstead(Window_Fade.Get());
        }

        protected override void OnStart() {
            // Start Open Game Logic
        }

        protected override void OnUpdate() {
            for (int i = 0; i < windows_opened.Count; i++)
                windows_opened[i].UpdateUI();

            // Skip Splash Screen
        }


        // Methods -> Public
        public void CloseAllWindows() {
            for (int i = 0; i < windows_opened.Count; i++)
                windows_opened[i].CloseWindow(0, 0);
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
    }
}