using System;
using System.Collections;
using UnityEngine;

namespace JamCat.UI 
{
    public enum FadeType { fade_in, fade_out }
    public class AutoCanvasGroup : MonoBehaviour {
        
        // Variables -> Public
        public FadeType fade_type;
        public CanvasGroup canvas_group;
        public float fade_value;
        public float fade_time;
        public float execute_in;

        // Variables -> Private
        private bool finished;

        // Methods -> Standard
        private void Update() {
            // Se terminou, destrói este Script
            if (finished == true)
                Destroy(this);

            // Apenas vai mudar o valor do fade quando o valor do execute_in for 0
            if (execute_in > 0) {
                execute_in -= Time.deltaTime;
                return;
            }

            // Muda o valor do fade conforme o tipo de fade
            switch(fade_type){
                case FadeType.fade_in:
                    fade_value += Time.deltaTime / fade_time;

                    if (fade_value >= 1) {
                        canvas_group.blocksRaycasts = true;
                        finished = true;
                    }
                    break;
                case FadeType.fade_out:
                    fade_value -= Time.deltaTime / fade_time;
                    canvas_group.blocksRaycasts = false;
                    if (fade_value <= 0)
                        finished = true;
                    break;
            }

            // Muda o falor de fade do CanvasGroup
            canvas_group.alpha = fade_value;
        }

        // Methods -> Public
        public void SetFade(UI ui, FadeType fade_type, float fade_time, float execute_in) {
            canvas_group = ui.GetComponent<CanvasGroup>();
            fade_value = canvas_group.alpha;
            this.fade_time = fade_time;
            this.execute_in = execute_in;
            this.fade_type = fade_type;

            if (finished == true)
                finished = false;
        }

        public void SetFadeIn(UI ui, float fade_time, float execute_in) {
            canvas_group = ui.GetComponent<CanvasGroup>();
            fade_value = canvas_group.alpha;
            this.fade_time = fade_time;
            this.execute_in = execute_in;
            fade_type = FadeType.fade_in;
            finished = false;
        }

        public void SetFadeOut(UI ui, float fade_time, float execute_in) {
            canvas_group = ui.GetComponent<CanvasGroup>();
            fade_value = canvas_group.alpha;
            this.fade_time = fade_time;
            this.execute_in = execute_in;
            fade_type = FadeType.fade_out;
            finished = false;
        }

    }
}