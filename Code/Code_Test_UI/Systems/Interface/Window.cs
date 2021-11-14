using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Window : UI
{
    // Variables
    public bool is_opened;
    public CanvasGroup canvasGroup;
    public UI[] ui_elements;

    // Methods -> Abstract
    protected abstract void OnAwakeWindow();
    protected abstract void OnStartWindow();
    protected abstract void OnUpdateWindow();

     // Methods -> Override
    protected override void OnAwake() {
        OnAwakeWindow();
        for (int i = 0; i < ui_elements.Length; i++)
            ui_elements[i].AwakeUI();
    }

    protected override void OnStart() {
        OnStartWindow();
        for (int i = 0; i < ui_elements.Length; i++)
            ui_elements[i].StartUI();
    }

    protected override void OnUpdate() {
        OnUpdateWindow();
        for (int i = 0; i < ui_elements.Length; i++)
            ui_elements[i].UpdateUI();
    }

    // Methods -> Public
    public void Toggle() {
        if (is_opened == true)
            Close();
        else
            Open();
    }

    public void Open() {
        is_opened = true;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close() {
        is_opened = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}