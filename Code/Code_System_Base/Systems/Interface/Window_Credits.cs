using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Window_Credits : Window
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
    public void Button_Back() {
        UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
        Window_MainMenu window = Window_MainMenu.Get() as Window_MainMenu;
        UI_Methods.SetFade(window, FadeType.fade_in, 0.5f, 0.5f);
    }
}