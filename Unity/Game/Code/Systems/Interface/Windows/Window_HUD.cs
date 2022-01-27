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
        [Header("Configuration")]
        public UI_Bar barMana;
        public UI_Bar_Array barLife;
        public Window windowControlsInfo;

        [Header("Run-Time")]
        public int currenct_character;

        private bool triggerControlInfo;
        public float timerControlsInfo;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnUpdateWindow() {
            if (triggerControlInfo == true) {
                timerControlsInfo -= Time.deltaTime;
                if (timerControlsInfo <= 0) {
                    triggerControlInfo = false;
                    windowControlsInfo.CloseWindow(1f, 0);
                }
            }
        }

        protected override void OnOpenWindow() {
            windowControlsInfo.CloseWindow(0, 0);
            windowControlsInfo.OpenWindow(1f, 0);
            timerControlsInfo = 4f;
            triggerControlInfo = true;
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