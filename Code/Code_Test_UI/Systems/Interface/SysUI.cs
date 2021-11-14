using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysUI : Sys
{
    // Variables
    public Window[] windows;

    public Window onStartOpenWindow;

    // Methods -> Override
    protected override void OnAwake() {
        CloseAllWindows();
        for (int i = 0; i < windows.Length; i++)
            windows[i].AwakeUI();
    }

    protected override void OnStart() {
        onStartOpenWindow.Open();        
        for (int i = 0; i < windows.Length; i++)
            windows[i].StartUI();
    }

    protected override void OnUpdate() {
        for (int i = 0; i < windows.Length; i++)
            windows[i].UpdateUI();
    }

    // Methods -> Public
    public void CloseAllWindows() {
        for (int i = 0; i < windows.Length; i++)
            windows[i].Close();
    }
}
