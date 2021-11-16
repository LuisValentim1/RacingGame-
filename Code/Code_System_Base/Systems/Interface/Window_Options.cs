using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Window_Options : Window
{
    // Variables
    public Window last_window;

    public CanvasGroup cv_graphics;
    public CanvasGroup cv_audio;
    public CanvasGroup cv_controls;

    // Methods -> Override
    protected override void OnAwakeWindow() {

    }

    protected override void OnStartWindow() {

    }

    protected override void OnUpdateWindow() {

    }

    // Methods -> Public
    public void StartOptions(Window last_window) {
        last_window = this.last_window;
    }

    public void Button_Graphics() {
        throw new NotImplementedException();
    }

    public void Button_Audio() {
        throw new NotImplementedException();
    }
    
    public void Button_Controls() {
        throw new NotImplementedException();
    }

    public void Button_Apply() {
        throw new NotImplementedException();
    }

    public void Button_Back() {
        UI_Methods.SetFade(this, FadeType.fade_out, 0.5f, 0);
        UI_Methods.SetFade(last_window, FadeType.fade_in, 0.5f, 0.5f);
        last_window = null;
    }


    public void Save() {
        string path = Application.persistentDataPath + "/options.dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fStream = File.Create(path);
        bf.Serialize(fStream, Data.options);
        fStream.Close();
    }

    public void Load() {
        throw new NotImplementedException();
    }

    public void Apply() {
        throw new NotImplementedException();
    }

    public void LoadAndApply() {
        Load();
        Apply();
    }
}