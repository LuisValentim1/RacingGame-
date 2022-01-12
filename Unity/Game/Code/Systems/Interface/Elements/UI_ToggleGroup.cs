using System;
using System.Collections;
using UnityEngine;

namespace JamCat.UI {
    public class UI_ToggleGroup : UI {
        
        // Variables
        [SerializeField] private UI_Toggle[] toggles;

        // Methods -> Standard
        protected override void OnAwake() {
            for (int i = 0; i < toggles.Length; i++)
                toggles[i].AwakeUI();
        }

        protected override void OnUpdate() {

        }

        protected override void OnOpen() {

        }

        protected override void OnClose() {

        }

        // Methods -> Public
        public void ActivateToggle(UI_Toggle toggle) {
            for (int i = 0; i < toggles.Length; i++)
                if (toggles[i] != toggle)
                    toggles[i].Activate(false);
                else
                    toggles[i].Activate(true);
        }

        public void DeactivateAll() {
            for (int i = 0; i < toggles.Length; i++)
                toggles[i].Activate(false);
        }
    }
}