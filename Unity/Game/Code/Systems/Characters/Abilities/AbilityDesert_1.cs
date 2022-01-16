using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

namespace JamCat.Characters 
{
    public class AbilityDesert_1 : Ability
    {
        public GameObject prefabPojectile;
        private List<Projectile> projectiles;
        public GameObject prefabAbilityDesert;
        private List<GameObject> abilities;
       
        public float projectileTimer = 0.6f;
        public RuntimeAnimatorController projectileAnimatorGraphics;

        // Methods -> Override
        protected override void OnAwake() {
            projectiles = new List<Projectile>();
            abilities = new List<GameObject>();
        }

        protected override void OnStart() {
            
        }

        protected override void OnUpdate() {
            for (int i = 0; i < projectiles.Count; i++) {
                if (projectiles[i].getEnded() == true) {
                    Projectile projectile = projectiles[i];
                    OnProjectileEnd(projectile);
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
            GameObject newObj = Instantiate(prefabPojectile, transform.position, transform.rotation, null);
            Projectile projectile = newObj.GetComponent<Projectile>();
            projectile.setProjectile(projectileTimer, GetComponentInParent<TopDownCarController>().getVelocity() + 10);
            projectile.setGraphics(projectileAnimatorGraphics);
            projectiles.Add(projectile);
        }


        public void OnProjectileEnd(Projectile projectile) {
            GameObject newObj = Instantiate(prefabAbilityDesert, projectile.spawnPoint.position, projectile.transform.rotation, null);
            abilities.Add(newObj);
        }
    }
}
