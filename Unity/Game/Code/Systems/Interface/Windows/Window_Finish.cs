using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JamCat.UI 
{
    public class Window_Finish : Window
    {
        // Variables -> Instance
        public static Window_Finish instance;
        public static Window_Finish Get() { return instance; }

        // Variables

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
        public void Button_PlayAgain() {
            CloseWindow(0.5f, 0);

            if (Data.Get().gameData.localMode == false)
                SysMultiplayer.Get().multiplayerMethods.StartGameServerRpc();
            else 
                GeneralMethods.StartGame();
        }
        
        public void Button_MainMenu() {
            Data.Get().gameData.characterSelected = -1;
            CloseWindow(0.5f, 0);
            Window_MainMenu.Get().OpenWindow(0.5f, 0.5f);
            GeneralMethods.MainMenu();

            if (Data.Get().gameData.localMode == false)
                SysMultiplayer.Get().Disconnect();
        }
    }
}