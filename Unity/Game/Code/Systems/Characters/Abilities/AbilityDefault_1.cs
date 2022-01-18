using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Characters 
{
    public class AbilityDefault_1 : Ability
    {
        public Transform spawnPivot;
        public GameObject prefabOil;

        // Methods -> Override
        protected override void OnAwake() {

        }

        protected override void OnStart() {

        }

        protected override void OnUpdate() {

        }

        protected override void OnUse() {
            Instantiate(prefabOil, spawnPivot.transform.position, Quaternion.identity);
        }

        protected override string[] OnSendInfo() {

            return null;
        }

        protected override void OnGetInfo(string[] info) {

        }
    }
}
