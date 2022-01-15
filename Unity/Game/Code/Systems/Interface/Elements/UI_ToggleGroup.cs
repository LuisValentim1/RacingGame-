using System;
using System.Collections;
using UnityEngine;

namespace JamCat.UI {
    public class UI_ToggleGroup : UI {
        
        // Variables
        public bool interactable;
        public UI_Toggle toggleActivated;
        [SerializeField] public UI_Toggle[] toggles;

        // Methods -> Standard
        protected override void OnAwake() {
            for (int i = 0; i < toggles.Length; i++)
                toggles[i].AwakeUI();
        }

        protected override void OnUpdate() {
            for (int i = 0; i < toggles.Length; i++)
                toggles[i].UpdateUI();
        }

        protected override void OnOpen() {
            setServerEnabledAll(true);
            toggleActivated = null;
            for (int i = 0; i < toggles.Length; i++)
                toggles[i].OpenUI();
        }

        protected override void OnClose() {
            for (int i = 0; i < toggles.Length; i++)
                toggles[i].CloseUI();
        }

        // Methods -> Public
        public void ActivateAll(bool value) {
            for (int i = 0; i < toggles.Length; i++)
                toggles[i].Activate(value);
            CheckActive();
        }

        public void ActivateToggle(int number, bool value) {
            toggles[number].Activate(value);
            CheckActive();
        }

        public void ActivateToggle(UI_Toggle toggle) {
            for (int i = 0; i < toggles.Length; i++)
                if (toggles[i] != toggle) {
                    toggles[i].Activate(false);
                } else {
                    toggles[i].Activate(true);
                    toggleActivated = toggles[i];
                }
            CheckActive();
        }

        public void setServerEnabledAll(bool value) {
            for (int i = 0; i < toggles.Length; i++)
                toggles[i].setServerEnabled(value);
        }

        public void setServerEnabled(int id, bool value) {
            if (id >= 0)
                toggles[id].setServerEnabled(value);
        }

        public int getToggleNumberActivated() {
            if (toggleActivated == null)
                return -1;

            for (int i = 0; i < toggles.Length; i++)
                if (toggles[i] == toggleActivated)
                    return i;

            return -1;
        }

        private void CheckActive() {
            for (int i = 0; i < toggles.Length; i++) {
                if (toggles[i].activated == true) {
                    toggleActivated = toggles[i];
                    return;
                }
            }

            toggleActivated = null;
        }
    }
}