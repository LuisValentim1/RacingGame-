using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Window_Options : Window
{
    // Variables -> Instance
    public static Window_Options instance;
    public static Window_Options Get() { return instance; }

    // Variables
    public Window last_window;

    public CanvasGroup[] cvs;

    // Methods -> Override
    protected override void OnAwakeWindow() {
        instance = this;
    }

    protected override void OnOpenWindow() {

    }
    
    protected override void OnCloseWindow() {

    }

    protected override void OnUpdateWindow() {

    }

    // Methods -> Public
    public void StartOptions(Window last_window) {
        last_window = this.last_window;
        Load();
    }

    public void Button_Graphics() {
        CloseAllWindows_Instead(cvs[0]);
    }

    public void Button_Audio() {
        CloseAllWindows_Instead(cvs[1]);
    }
    
    public void Button_Controls() {
        CloseAllWindows_Instead(cvs[2]);
    }

    public void Button_Apply() {
        Save();
        Apply();
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
        // throw new NotImplementedException();
    }

    public void Apply() {
        // throw new NotImplementedException();
    }

    public void LoadAndApply() {
        Load();
        Apply();
    }

    public void CloseAllWindows_Instead(CanvasGroup less) {
        for (int i = 0; i < cvs.Length; i++) {
            if (cvs[i] != less) {
                cvs[i].alpha = 0;
                cvs[i].blocksRaycasts = false;
            }
        }

        less.alpha = 1;
        less.blocksRaycasts = true;
    }
}