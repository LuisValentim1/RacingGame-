using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Characters 
{
    abstract public class Ability : MonoBehaviour 
    {
        // Variables -> Public
        protected Character character;
        public float cooldown;

        private float curCooldown;

        // Methods -> Abstract
        protected abstract void OnAwake();
        protected abstract void OnStart();
        protected abstract void OnUpdate();
        protected abstract void OnUse();

        // Methods -> Public
        public void AwakeAbility() {
            this.character = GetComponentInParent<Character>();
            OnAwake();
        }

        public void StartAbility() {
            OnStart();
        }

        public void UpdateAbility() {
            OnUpdate();

            if (curCooldown > 0)
                curCooldown -= Time.deltaTime;
        }

        public void UseAbility() {
            if (curCooldown > 0)
                return;

            curCooldown = cooldown;
            OnUse();
        }
    }
}