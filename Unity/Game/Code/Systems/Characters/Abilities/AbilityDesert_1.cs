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
                    Destroy(projectile.gameObject);
                }
            }
        }

        protected override void OnUse() {
            GameObject newObj = Instantiate(prefabPojectile, transform.position, transform.rotation, null);
            newObj.GetComponent<Projectile>().setProjectile(projectileTimer, GetComponentInParent<TopDownCarController>().getVelocity() + 10);
            projectiles.Add(newObj.GetComponent<Projectile>());
        }


        public void OnProjectileEnd(Projectile projectile) {
            GameObject newObj = Instantiate(prefabAbilityDesert, projectile.transform.position, projectile.transform.rotation, null);
            abilities.Add(newObj);
        }
    }
}
