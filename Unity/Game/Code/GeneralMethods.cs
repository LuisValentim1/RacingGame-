using System;
using System.Collections;
using UnityEngine;

public static class GeneralMethods {

    public static void StartGame() {
        Data.gameLogic.in_main_menu = false;
        Data.gameLogic.in_game = true;
    }

    public static void PauseGame(bool state) {
        Data.gameLogic.is_paused = state;
    }

    public static void MainMenu() {
        Data.gameLogic.in_main_menu = true;
        Data.gameLogic.in_game = false;
    }
}