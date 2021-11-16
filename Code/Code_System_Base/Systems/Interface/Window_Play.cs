using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Window_Play : Window
{
    // Variables
    public int character_selected = -1;

    // Methods -> Override
    protected override void OnAwakeWindow() {

    }

    protected override void OnStartWindow() {

    }

    protected override void OnUpdateWindow() {

    }

    // Methods -> Public
    public void Button_Play() {
        if (character_selected < 0)
            return;

        UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
        Data.Get().in_game = true;
    }

    public void Button_Select_Character(int number) {
        character_selected = number;
    }
    
    public void Button_Back() {
        UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
        Window_MainMenu window = Window_MainMenu.Get() as Window_MainMenu;
        UI_Methods.SetFade(window, FadeType.fade_in, 0.5f, 0);
    }
}