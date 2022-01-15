using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Characters 
{
    public class AbilityWater_1 : Ability
    {
        // Variables
        public float duration;
        private bool used;
        private float timer;

        // Methods -> Override
        protected override void OnAwake() {

        }

        protected override void OnStart() {

        }

        protected override void OnUpdate() {
            if (used == true) {
                timer += Time.deltaTime;

                if (timer >= duration) {
                    character.usingShield = false;
                    used = false;
                }
            }
        }

        protected override void OnUse() {
            used = true;
            timer = 0;
            character.usingShield = true;
        }
    }
}
