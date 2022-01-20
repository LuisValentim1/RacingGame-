using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

namespace JamCat.Characters 
{
    public class AbilitySpace_1 : Ability
    {
        public GameObject prefabPojectile;
        private List<Projectile> projectiles;
       
        public Transform transfSpawn;
        public float projectileTimer = 0.6f;
        public float duration;
        public float speed = 10;
        public RuntimeAnimatorController projectileAnimatorGraphics;

        // Methods -> Override
        protected override void OnAwake() {
            projectiles = new List<Projectile>();
        }

        protected override void OnStart() {
            
        }

        protected override void OnUpdate() {
            for (int i = 0; i < projectiles.Count; i++) {
                if (projectiles[i].getEnded() == true) {
                    Projectile projectile = projectiles[i];
                    projectiles.Remove(projectile);
                }
            }
        }

        protected override string[] OnSendInfo() {
            return null;
        }

        protected override void OnGetInfo(string[] info) {
            
        }

        protected override void OnUse() {
            GameObject newObj = Instantiate(prefabPojectile, transfSpawn.transform.position, transform.rotation, null);
            Projectile projectile = newObj.GetComponent<Projectile>();
            AOil aOil = projectile.gameObject.AddComponent<AOil>();
            aOil.timerEffect = duration;
            aOil.countdownToDestroy = 5;
            aOil.setSelfDestroyOnHit(true, "Player");
            projectile.setProjectile(projectileTimer, GetComponentInParent<TopDownCarController>().getVelocity() + speed);
            projectile.setGraphics(projectileAnimatorGraphics);
            projectiles.Add(projectile);
        }
    }
}
