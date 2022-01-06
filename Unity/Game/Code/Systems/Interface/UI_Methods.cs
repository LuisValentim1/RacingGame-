using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamCat.UI 
{
    public static class UI_Methods
    {
        // Methods -> Fade
        public static void SetFade(UI ui, FadeType fade_type, float fade_time, float execute_in) {
            if (ui.GetComponent<AutoCanvasGroup>() == null)
                ui.gameObject.AddComponent<AutoCanvasGroup>().SetFade(ui, fade_type, fade_time, execute_in);
            else
                ui.gameObject.GetComponent<AutoCanvasGroup>().SetFade(ui, fade_type, fade_time, execute_in);
        }

        public static void SetFadeIn(UI ui, float fade_time, float execute_in) {
        if (ui.GetComponent<AutoCanvasGroup>() == null)
                ui.gameObject.AddComponent<AutoCanvasGroup>().SetFadeIn(ui, fade_time, execute_in);
            else
                ui.gameObject.GetComponent<AutoCanvasGroup>().SetFadeIn(ui, fade_time, execute_in);
        }

        public static void SetFadeOut(UI ui, float fade_time, float execute_in) {
        if (ui.GetComponent<AutoCanvasGroup>() == null)
                ui.gameObject.AddComponent<AutoCanvasGroup>().SetFadeOut(ui, fade_time, execute_in);
            else
                ui.gameObject.GetComponent<AutoCanvasGroup>().SetFadeOut(ui, fade_time, execute_in);
        }


        // Methods -> Window
    }
}