using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_MainMenu : Window
{
    // Variables -> Instance

    // Variables
    public Button button_play;
    public Button button_options;
    public Button button_credits;
    public Button button_quit;

    private bool quit;

    // Methods -> Override
    protected override void OnAwakeWindow() {

    }

    protected override void OnStartWindow() {

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
        Window_Play window = Window_Play.Get() as Window_Play;
        UI_Methods.SetFade(window, FadeType.fade_in, 0.5f, 0.5f);
    }

    public void Button_Options() {
        UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
        Window_Options window = Window_Options.Get() as Window_Options;
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