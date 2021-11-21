using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Window_SplashScreen : Window
{
    // Variables -> Instance
    public static Window_SplashScreen instance;
    public static Window_SplashScreen Get() { return instance; }

    // Variables
    public bool skip;

    // Methods -> Override
    protected override void OnAwakeWindow() {
       instance = this;
       canvasGroup.alpha = 1;
       canvasGroup.blocksRaycasts = true;
    }

    protected override void OnOpenWindow() {
        UI_Methods.SetFade(this, FadeType.fade_out, 1f, 3f);
    }

    protected override void OnCloseWindow() {

    }

    protected override void OnUpdateWindow() {
        if (Input.anyKeyDown == true) {
            if (skip == false) {
                UI_Methods.SetFade(this, FadeType.fade_out, 1f, 0);
                skip = true;    
            }
        }
    }

    // Methods -> Public
}