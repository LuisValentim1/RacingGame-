using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Characters 
{
    public class AbilitySpace_2 : Ability
    {
        public Transform spawnPivot;
        public GameObject prefabPortal;

        // Methods -> Override
        protected override void OnAwake() {

        }

        protected override void OnStart() {

        }

        protected override void OnUpdate() {

        }

        protected override void OnUse() {
            Instantiate(prefabPortal, spawnPivot.transform.position, Quaternion.identity);
        }

        protected override string[] OnSendInfo() {

            return null;
        }

        protected override void OnGetInfo(string[] info) {

        }
    }
}
