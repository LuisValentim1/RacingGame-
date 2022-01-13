using System;
using System.Collections;
using UnityEngine;
using JamCat.UI;

namespace JamCat.Characters 
{
    public class Character : MonoBehaviour 
    {
        // Variables
        public string characterName;
        public Ability abilityBasic;
        public Ability abilityUlti;
        public CharacterLogic CharacterLogic;

        public bool isOut;
        public int maxLifes = 7;
        public int curLifes = 7;
        public float maxMana = 100;
        public float curMana = 0;


        // Methods -> Standard
        public void OnAwake() {
            abilityBasic.AwakeAbility();
            abilityUlti.AwakeAbility();
            Restart();
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

        public void Restart() {
            isOut = false;
            curMana = 0;
            curLifes = maxLifes;
            Window_HUD.Get().barMana.SetPercentage(curMana);
        }

        
        public void AddMana(float manaValue){
            curMana += Time.deltaTime * manaValue;
            if (curMana > maxMana)
                curMana = maxMana;

            Window_HUD.Get().barMana.SetPercentage(curMana);
            // print("From Character Class: " + curMana);
        }

        public void RemoveLife() {
            curLifes--;
            if (curLifes < 0) {
                isOut = true;
                curLifes = 0;
            }
        }
    }
}
