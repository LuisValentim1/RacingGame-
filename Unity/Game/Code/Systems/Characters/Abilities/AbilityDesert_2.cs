using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Characters 
{
    public class AbilityDesert_2 : Ability
    {
        public GameObject sprite;
        public float maxTime = 5;
        public float timer;

        // Methods -> Override
        protected override void OnAwake() {

        }

        protected override void OnStart() {

        }

        protected override void OnUpdate() {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                sprite.SetActive(false);
            }
        }

        protected override void OnUse() {
            timer = maxTime;
            sprite.SetActive(true);
        }
    }
}
