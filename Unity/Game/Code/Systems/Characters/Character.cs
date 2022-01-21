using System;
using System.Collections;
using UnityEngine;
using JamCat.UI;
using JamCat.Players;

namespace JamCat.Characters 
{
    public class Character : MonoBehaviour 
    {
        // Variables
        [Header("Configurable")]
        public string characterName;
        public AbilityGroup abilityGroup;
        public Sprite sprite;
        // public Sprite[] sprites;
        public int maxLifes = 7;
        public float maxMana = 100;

        [Header("Run-Time")]
        public Player player;
        public int curLifes = 7;
        public float curMana = 0;
        public bool isOut;
        public bool usingShield;


        // Methods -> Standard
        public void OnAwake(Player player) {
            this.player = player;
            abilityGroup.OnAwake();
            Restart();
        }

        public void OnStart() {
            abilityGroup.OnStart();
        }

        public void OnUpdate() {
            abilityGroup.OnUpdate();
        }

        public void OnHit() {
            if (usingShield == true) {
                if (abilityGroup.abilityBasic.GetType() == typeof(AbilityWater_1)) {
                    // print("Funciona!");
                    AbilityWater_1 ability = abilityGroup.abilityBasic as AbilityWater_1;
                    ability.OnHit();
                }
            }
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
            Window_HUD.Get().barLife.setValue(curLifes);
            Window_HUD.Get().barMana.SetPercentage(curMana);
            UpdateGraphics();
        }
        

        public void UpdateGraphics() {
            player.spriteRenderer.sprite = sprite;
        }


        public void AddMana(float manaValue){
            curMana += manaValue;
            if (curMana > maxMana)
                curMana = maxMana;

            Window_HUD.Get().barMana.SetPercentage(curMana);
        }

        public void AddManaByTime(float manaValue){
            curMana += Time.deltaTime * manaValue;
            if (curMana > maxMana)
                curMana = maxMana;

            Window_HUD.Get().barMana.SetPercentage(curMana);
        }
        
        public void RemoveMana(float manaValue){
            curMana -= manaValue;
            if (curMana < 0)
                curMana = 0;

            Window_HUD.Get().barMana.SetPercentage(curMana);
        }

        public void RemoveLife(int value) {
            if (usingShield == true)
                return;

            curLifes -= value;
            if (curLifes <= 0) {
                isOut = true;
                curLifes = 0;
            }

            if (player.getNetworkObject().IsLocalPlayer)
                Window_HUD.Get().barLife.setValue(curLifes);
        }

        public void RemoveLife() {
            if (usingShield == true)
                return;

            curLifes--;
            if (curLifes <= 0) {
                isOut = true;
                curLifes = 0;
            }

            if (player.getNetworkObject().IsLocalPlayer)
                Window_HUD.Get().barLife.setValue(curLifes);
        }
    }
}
