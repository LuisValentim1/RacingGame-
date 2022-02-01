using System;
using System.Collections;
using UnityEngine;
using JamCat.Players;
using JamCat.Cameras;
using JamCat.UI;
using JamCat.Audio;
using JamCat.Map;

public static class GeneralMethods {

    public static void StartGame() {
        PauseGame(false);
        Data.Get().gameLogic.in_main_menu = false;
        Data.Get().gameLogic.in_game = true;
        Data.Get().gameLogic.game_finished = false;
        Data.Get().sceneConfiguration.scene_play.SetActive(true);
        Data.Get().sceneConfiguration.scene_main_menu.SetActive(false);
        
        SysUI.Get().CloseAllWindows();
        Window_CharacterSelection.Get().CloseWindow(0.2f, 0);
        Window_HUD.Get().OpenWindow(0.2f, 0);

        SysMap.Get().RestartMap();
        SysMap.Get().GenerateMap();

        SysPlayer.Get().Restart();

        if (Data.Get().gameData.localMode == false)
            SysCamera.Get().SetPlayerTarget(SysPlayer.Get().localPlayerObj.transform);
        else
            SysCamera.Get().SetPlayerTarget(SysPlayer.Get().getLocalPlayer(0).transform);

        AudioMusic.Get().PlayMusic(1);
        SysCamera.Get().SetCamera(1);

        GeneralMethods.StartCountdown(3);

        // Terminar para os outros sistemas
    }

    public static void StartCountdown(int seconds) {
        Data.Get().gameLogic.countdown = seconds;
        Window_HUD.Get().StartCountdown();
    }

    public static void PauseGame(bool state) {
        if (Data.Get().gameLogic.game_finished == true)
            return;

        if (Data.Get().gameLogic.in_game == false)
            return;

        // Debug.Log("Pause was called: " + state);
        Data.Get().gameLogic.is_paused = state;
        if (state == true) {
            Window_PauseMenu.Get().OpenWindow(0.2f, 0);
        } else {
            Window_PauseMenu.Get().CloseWindow(0.2f, 0);
        }
    }

    public static void MainMenu() {
        PauseGame(false);
        Data.Get().gameLogic.in_main_menu = true;
        Data.Get().gameLogic.in_game = false;
        Data.Get().sceneConfiguration.scene_main_menu.SetActive(true);
        Data.Get().sceneConfiguration.scene_play.SetActive(false);
        SysMap.Get().RestartMap();
        SysPlayer.Get().Restart();
        AudioMusic.Get().PlayMusic(0);
        SysCamera.Get().SetCamera(0);

        // Terminar para os outros sistemas
    }

    public static void CallFinish(int characterNumber) {
        Data.Get().gameLogic.game_finished = true;
        Window_Finish.Get().OpenWindow(0.2f, 0);
        Window_Finish.Get().UpdateCharacterPic(characterNumber);

        if (Data.Get().gameLogic.is_paused == true)
            Window_PauseMenu.Get().CloseWindow(0.3f, 0);
    }




    

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static Vector3 Remap(Vector3 value, Vector3 from1, Vector3 to1, Vector3 from2, Vector3 to2)
    {
        float x = Remap(value.x, from1.x, to1.x, from2.x, to2.x);
        float y = Remap(value.y, from1.y, to1.y, from2.y, to2.y);
        float z = Remap(value.z, from1.z, to1.z, from2.z, to2.z);
        return new Vector3(x, y, z);
    }

    public static float RemapLimitBefore(float value, float from1, float to1, float from2, float to2)
    {
        if (value < from1)
            return from2;
        if (value > to1)
            return to2;
        else
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static float RemapLimitAfter(float value, float from1, float to1, float from2, float to2)
    {
        float newValue = (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        if (newValue < from1)
            return from2;
        if (newValue > to1)
            return to2;
        else
            return newValue;
    }
}