using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamCat.Characters 
{
    public class AbilityGroup : MonoBehaviour
    {
        public Ability abilityBasic;
        public Ability abilityUlti;

        public void OnAwake() {
            abilityBasic.AwakeAbility();
            abilityUlti.AwakeAbility();
        }

        public void OnStart() {
            abilityBasic.StartAbility();
            abilityUlti.StartAbility();
        }

        public void OnUpdate() {
            abilityBasic.UpdateAbility();
            abilityUlti.UpdateAbility();
        }

        // Methods -> Publlic
        public void UseAbilityBasic() {
            abilityBasic.UseAbility();
        }

        public void UseAbilityUlti() {
            abilityUlti.UseAbility();
        }
    }
}