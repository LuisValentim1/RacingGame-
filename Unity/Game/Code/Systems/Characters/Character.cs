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
        public AbilityGroup abilityGroup;
        public CharacterLogic CharacterLogic;

        public Sprite[] sprites;

        public bool isOut;
        public int maxLifes = 7;
        public int curLifes = 7;
        public float maxMana = 100;
        public float curMana = 0;


        // Methods -> Standard
        public void OnAwake() {
            abilityGroup.OnAwake();
            Restart();
        }

        public void OnStart() {
            abilityGroup.OnStart();
        }

        public void OnUpdate() {
            abilityGroup.OnUpdate();
        }

        // Methods -> Publlic
        public void UseAbilityBasic() {
            abilityGroup.UseAbilityBasic();
        }

        public void UseAbilityUlti() {
            abilityGroup.UseAbilityUlti();
        }

        public void Restart() {
            isOut = false;
            curMana = 0;
            curLifes = maxLifes;
            Window_HUD.Get().barLife.SetPercentage(getLifePercentage());
            Window_HUD.Get().barMana.SetPercentage(curMana);

            if (Data.Get().gameData.characterSelected >= 0)
                GetComponentInChildren<SpriteRenderer>().sprite = sprites[Data.Get().gameData.characterSelected];
        }

          public void AddMana(float manaValue){
            curMana += manaValue;
            if (curMana > maxMana)
                curMana = maxMana;

            Window_HUD.Get().barMana.SetPercentage(curMana);
            // print("From Character Class: " + curMana);
        }

        public void AddManaByTime(float manaValue){
            curMana += Time.deltaTime * manaValue;
            if (curMana > maxMana)
                curMana = maxMana;

            Window_HUD.Get().barMana.SetPercentage(curMana);
            // print("From Character Class: " + curMana);
        }
        
        public void RemoveMana(float manaValue){
            curMana -= manaValue;
            if (curMana < 0)
                curMana = 0;

            Window_HUD.Get().barMana.SetPercentage(curMana);
            // print("From Character Class: " + curMana);
        }

        public void RemoveLife() {
            curLifes--;
            if (curLifes <= 0) {
                isOut = true;
                curLifes = 0;
            }
            Window_HUD.Get().barLife.SetPercentage(getLifePercentage());
        }


        

        public void UpdateGraphics(int character) {
            if (character >= 0)
                GetComponentInChildren<SpriteRenderer>().sprite = sprites[character];
        }

        public float getLifePercentage() {
            return (100f / maxLifes) * curLifes;
        }
    }
}
