using System;
using System.Collections;
using UnityEngine;
using JamCat.Players;

namespace JamCat.Characters 
{
    public class AbilityDefault_2 : Ability
    {
        public Animator animator;
        public float force, duration;
        
        // Methods -> Override
        protected override void OnAwake() {

        }

        protected override void OnStart() {

        }

        protected override void OnUpdate() {

        }

        protected override void OnUse() {
            GetComponentInParent<TopDownCarController>().ApplyDashForce(duration, transform.up, force);
            animator.SetTrigger("Activated");
        }

        protected override string[] OnSendInfo() {

            return null;
        }

        protected override void OnGetInfo(string[] info) {

        }
    }
}
