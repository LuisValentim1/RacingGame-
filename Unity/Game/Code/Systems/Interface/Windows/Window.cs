using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamCat.UI 
{
    abstract public class Window : UI
    {
        // Variables
        public bool is_opened;
        public CanvasGroup canvasGroup;
        public UI[] ui_elements;

        // Methods -> Abstract
        protected abstract void OnAwakeWindow();
        protected abstract void OnOpenWindow();
        protected abstract void OnCloseWindow();
        protected abstract void OnUpdateWindow();

        // Methods -> Override
        protected override void OnAwake() {
            OnAwakeWindow();
            for (int i = 0; i < ui_elements.Length; i++)
                ui_elements[i].AwakeUI();
        }

        protected override void OnOpen() {
            OnOpenWindow();
            for (int i = 0; i < ui_elements.Length; i++)
                ui_elements[i].OpenUI();

            is_opened = true;
            SysUI sysUI = SysUI.Get() as SysUI;
            sysUI.windows_opened.Add(this);
        }

        protected override void OnClose() {
            OnCloseWindow();
            for (int i = 0; i < ui_elements.Length; i++)
                ui_elements[i].CloseUI();

            is_opened = false;
            SysUI sysUI = SysUI.Get();
            if (sysUI.windows_opened.Contains(this) == true)
                sysUI.windows_opened.Remove(this);
        }

        protected override void OnUpdate() {
            OnUpdateWindow();
            for (int i = 0; i < ui_elements.Length; i++)
                ui_elements[i].UpdateUI();
        }

        // Methods -> Public
        public void OpenWindow(float fade_time, float execute_in) {
            UI_Methods.SetFade(this, FadeType.fade_in, fade_time, execute_in);
            OnOpen();
        }
        
        public void CloseWindow(float fade_time, float execute_in) {
            UI_Methods.SetFade(this, FadeType.fade_out, fade_time, execute_in);
            OnClose();
        }

        public void Toggle() {
            if (is_opened == true)
                CloseWindow(0.3f, 0);
            else
                OpenWindow(0.3f, 0);
        }
    }
}