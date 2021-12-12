using System;
using System.Collections;
using UnityEngine;
using CatJam.Player;
using CatJam.Cameras;
using CatJam.UI;
using CatJam.Audio;
using CatJam.Map;

public static class GeneralMethods {

    public static void StartGame() {
        PauseGame(false);
        Data.Get().gameLogic.in_main_menu = false;
        Data.Get().gameLogic.in_game = true;
        Data.Get().sceneConfiguration.scene_play.SetActive(true);
        Data.Get().sceneConfiguration.scene_main_menu.SetActive(false);
        
        SysMap.Get().RestartMap();
        SysMap.Get().GenerateMap();
        SysPlayer.Get().RestartPlayer();
        SysCamera.Get().AutoConfigureCamera();
        SysAudio.Get().PlayGameMusic();

        // Terminar para os outros sistemas
    }

    public static void PauseGame(bool state) {
        Debug.Log("Pause was called: " + state);
        Data.Get().gameLogic.is_paused = state;

        if (state == true) {
            Window_PauseMenu.Get().OpenWindow(0.2f, 0);
            Time.timeScale = 0;
        } else {
            Window_PauseMenu.Get().CloseWindow(0.2f, 0);
            Time.timeScale = 1;
        }
    }

    public static void MainMenu() {
        PauseGame(false);
        Data.Get().gameLogic.in_main_menu = true;
        Data.Get().gameLogic.in_game = false;
        Data.Get().sceneConfiguration.scene_main_menu.SetActive(true);
        Data.Get().sceneConfiguration.scene_play.SetActive(false);
        SysMap.Get().RestartMap();
        SysAudio.Get().PlayMainMenuMusic();

        // Terminar para os outros sistemas
    }

    public static void CallFinish() {
        Data.Get().gameLogic.is_paused = true;
        Time.timeScale = 0;
        Window_Finish.Get().OpenWindow(0.2f, 0);
    }
}