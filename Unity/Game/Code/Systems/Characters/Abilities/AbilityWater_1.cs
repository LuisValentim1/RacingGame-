using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Characters 
{
    public class AbilityWater_1 : Ability
    {
        // Variables
        public GameObject objShield;
        public float duration;
        private bool used;
        private float timer;

        // Methods -> Override
        protected override void OnAwake() {
            objShield.SetActive(false);
        }

        protected override void OnStart() {

        }

        protected override void OnUpdate() {
            if (used == true) {
                timer += Time.deltaTime;

                if (timer >= duration) {
                    character.usingShield = false;
                    used = false;
                    objShield.SetActive(false);
                }
            }
        }

        protected override void OnUse() {
            used = true;
            timer = 0;
            character.usingShield = true;
            objShield.SetActive(true);
        }

        protected override string[] OnSendInfo() {

            return null;
        }

        protected override void OnGetInfo(string[] info) {

        }

        public void OnHit() {
            character.usingShield = false;
            used = false;
            objShield.SetActive(false);
        }
    }
}
