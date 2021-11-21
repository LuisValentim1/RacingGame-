using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_MainMenu : Window
{
    // Variables -> Instance
    public static Window_MainMenu instance;
    public static Window_MainMenu Get() { return instance; }

    // Variables
    private bool quit;

    // Methods -> Override
    protected override void OnAwakeWindow() {
        instance = this;
    }

    protected override void OnOpenWindow() {

    }
    
    protected override void OnCloseWindow() {

    }

    protected override void OnUpdateWindow() {


        if (quit == true) {
            Window_Fade window = Window_Fade.Get() as Window_Fade;
            UI_Methods.SetFade(window, FadeType.fade_out, 1f, 0);
            if (window.canvasGroup.alpha <= 0)
                Application.Quit();
        }
    }

    // Methods -> Public
    public void Button_Play() {
        UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
        Window_CharacterSelection window = Window_CharacterSelection.Get() as Window_CharacterSelection;
        UI_Methods.SetFade(window, FadeType.fade_in, 0.5f, 0.5f);
    }

    public void Button_Options() {
        UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
        Window_Options window = Window_Options.Get() as Window_Options;
        window.StartOptions(this);
        UI_Methods.SetFade(window, FadeType.fade_in, 0.5f, 0.5f);
    }

    public void Button_Credits() {
        UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
        Window_Credits window = Window_Credits.Get() as Window_Credits;
        UI_Methods.SetFade(window, FadeType.fade_in, 0.5f, 0.5f);
    }

    public void Button_Quit() {
        quit = true;
    }
}