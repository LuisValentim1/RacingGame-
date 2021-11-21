using System;
using System.Collections;
using UnityEngine;

public static class GeneralMethods {

    public static void StartGame() {
        Data.Get().in_main_menu = false;
        Data.Get().in_game = true;
    }

    public static void PauseGame(bool state) {

    }

    public static void MainMenu() {
        
    }
}