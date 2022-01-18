using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Characters 
{
    public class AbilityWater_2 : Ability
    {
        public GameObject[] objWaterGuns;
        public float duration;
        private Animator[] animatorWaterGuns;
        private bool used;
        private float timer;


        // Methods -> Override
        protected override void OnAwake() {
            animatorWaterGuns = new Animator[objWaterGuns.Length];
            for (int i = 0; i < objWaterGuns.Length; i++) {
                animatorWaterGuns[i] = objWaterGuns[i].GetComponent<Animator>();
                objWaterGuns[i].SetActive(false);
            }
        }

        protected override void OnStart() {
            
        }

        protected override void OnUpdate() {
            if (used == true) {
                timer += Time.deltaTime;
    
                for (int i = 0; i < objWaterGuns.Length; i++)
                    animatorWaterGuns[i].SetBool("isActive", false);

                if (timer >= duration) {
                    used = false;
                    for (int i = 0; i < objWaterGuns.Length; i++) {
                        animatorWaterGuns[i].SetBool("isActive", false);
                        objWaterGuns[i].SetActive(false);
                    }
                }
            }
        }

        protected override void OnUse() {
            used = true;
            timer = 0;
            for (int i = 0; i < objWaterGuns.Length; i++) {
                objWaterGuns[i].SetActive(true);
                animatorWaterGuns[i].SetBool("isActive", true);
            }
        }

        protected override string[] OnSendInfo() {

            return null;
        }

        protected override void OnGetInfo(string[] info) {

        }
    }
}
