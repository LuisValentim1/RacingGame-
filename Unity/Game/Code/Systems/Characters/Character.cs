using System;
using System.Collections;
using UnityEngine;

namespace CatJam.Characters 
{
    public class Character : MonoBehaviour 
    {
        // Variables
        public string characterName;
        public Ability abilityBasic;
        public Ability abilityUlti;
        public CharacterLogic CharacterLogic;

        // Methods -> Standard
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
