using System;
using System.Collections;
using UnityEngine;
using CatJam.Player;
using CatJam.Cameras;
using CatJam.Audio;
using CatJam.Map;

public static class GeneralMethods {

    public static void StartGame() {
        Data.Get().gameLogic.in_main_menu = false;
        Data.Get().gameLogic.in_game = true;
        Data.Get().sceneConfiguration.scene_play.SetActive(true);
        Data.Get().sceneConfiguration.scene_main_menu.SetActive(false);
        
        SysMap.Get().GenerateMap();
        SysPlayer.Get().AutoConfigurePlayer();
        SysCamera.Get().AutoConfigureCamera();
        SysAudio.Get().PlayGameMusic();

        // Terminar para os outros sistemas
    }

    public static void PauseGame(bool state) {
        Data.Get().gameLogic.is_paused = state;
    }

    public static void MainMenu() {
        Data.Get().gameLogic.in_main_menu = true;
        Data.Get().gameLogic.in_game = false;
        Data.Get().sceneConfiguration.scene_main_menu.SetActive(true);
        Data.Get().sceneConfiguration.scene_play.SetActive(false);
        SysMap.Get().RestartMap();
        SysAudio.Get().PlayMainMenuMusic();

        // Terminar para os outros sistemas
    }
}