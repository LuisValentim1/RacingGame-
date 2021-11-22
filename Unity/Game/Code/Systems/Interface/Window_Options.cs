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
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fStream = File.Create(path);
        binary.Serialize(fStream, Data.options);
        fStream.Close();
    }

    public void Load() {
        string path = Application.persistentDataPath + "/Options.dat";

        if (File.Exists(path) == true) {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fStream = File.Open(path, FileMode.Open);
            Data.options = binary.Deserialize(fStream) as Data.Options;
        }
    }

    public void Apply() {
        // Quality Level
        QualitySettings.SetQualityLevel(Data.options.quality_level);

        // Audio


        // Textures
        switch (Data.options.textures_quality) {
            case 0:
                QualitySettings.masterTextureLimit = 2;
                break;
            case 1:
                QualitySettings.masterTextureLimit = 1;
                break;
            case 2:
                QualitySettings.masterTextureLimit = 0;
                break;
        }

        // Shadows
        if (Data.options.enable_shadows == true)
            QualitySettings.shadows = ShadowQuality.All;
        else
            QualitySettings.shadows = ShadowQuality.Disable;

        switch (Data.options.shadows_quality)
        {
            case 0: 
                QualitySettings.shadowDistance = 50;
                break;
            case 1:
                QualitySettings.shadowDistance = 100;
                break;
            case 2:
                QualitySettings.shadowDistance = 150;
                break;
        }

        // Anti Aliasing

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