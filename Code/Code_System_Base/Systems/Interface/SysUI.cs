using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysUI : Sys
{
    // Variables
    public Window[] windows_loaded;
    public List<Window> windows_opened;

    public Window onStartOpenWindow;


    // Methods -> Override
    protected override void OnAwake() {
        windows_opened = new List<Window>();
        CloseAllWindows();
        for (int i = 0; i < windows_opened.Count; i++)
            windows_opened[i].AwakeUI();
    }

    protected override void OnStart() {
        onStartOpenWindow.Open();        
        for (int i = 0; i < windows_opened.Count; i++)
            windows_opened[i].StartUI();
    }

    protected override void OnUpdate() {
        for (int i = 0; i < windows_opened.Count; i++)
            windows_opened[i].UpdateUI();
    }

    // Methods -> Public
    public void CloseAllWindows() {
        for (int i = 0; i < windows_opened.Count; i++)
            windows_opened[i].Close();
    }

    public void OpenWindow(Window target, bool close_previous) {
        if (close_previous == true){
            ClosePreviousWindow();
            target.Open();
        }else{
            target.Open();
        }
    }

    public void OpenWindow(Window target) {
        target.Open();
    }

    public void ClosePreviousWindow() {
        if (windows_opened.Count > 1)
            windows_opened[windows_opened.Count - 1].Close();
    }
}
