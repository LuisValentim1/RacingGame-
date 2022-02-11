using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JamCat.Players;

namespace JamCat.UI 
{
    public class Window_HUD : Window
    {
        // Variables -> Instance
        public static Window_HUD instance;
        public static Window_HUD Get() { return instance; }

        // Variables
        [Header("Configuration")]
        // public UI_Bar barMana;
        // public UI_Bar_Array barLife;
        public Window windowControlsInfo;

        public GameObject prefabCharLeft, prefabCharRight;
        public Transform parentCharLeft, parentCharRight;

        [Header("Run-Time")]
        public int currenct_character;

        private bool triggerControlInfo;
        public float timerControlsInfo;

        public UI_Character[] uiCharacters;


        public SpriteRenderer spriteRendererSemaforoBackground;
        public Animator animatorSemaforo;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnUpdateWindow() {
            if (Data.Get().gameLogic.countdown > 0) {
                spriteRendererSemaforoBackground.color = new Color(0, 0, 0, 0.65f);
            } else {
                spriteRendererSemaforoBackground.color = new Color(0, 0, 0, 0);
            }

            if (triggerControlInfo == true) {
                timerControlsInfo -= Time.deltaTime;
                if (timerControlsInfo <= 0) {
                    triggerControlInfo = false;
                    windowControlsInfo.CloseWindow(1f, 0);
                }
            }
        }

        protected override void OnOpenWindow() {
            windowControlsInfo.CloseWindow(0, 0);
            windowControlsInfo.OpenWindow(1f, 0);
            timerControlsInfo = 3f;
            triggerControlInfo = true;

            Restart();
        }

        protected override void OnCloseWindow() {

        }

        // Methods -> Public
        public void Configure() {
            
        }

        public void Button_PauseMenu() {
            Window_PauseMenu.Get().OpenWindow(0.2f, 0);
        }

        public void Restart() {
            for (int i = 0; i < uiCharacters.Length; i++)
                if (uiCharacters[i] != null)
                    Destroy(uiCharacters[i].gameObject);

            uiCharacters = new UI_Character[Data.Get().gameData.charactersSelected.Length];
        }

        public UI_Character AddCharacterLeft(int playerNumber, int characterNumber) {
            GameObject newObj = Instantiate(prefabCharLeft, parentCharLeft);
            UI_Character uiCharacter = newObj.GetComponent<UI_Character>();
            uiCharacter.ChooseCharacterImg(characterNumber);
            uiCharacters[playerNumber] = uiCharacter;
            return uiCharacter;
        }

        public UI_Character AddCharacterRight(int playerNumber, int characterNumber) {
            GameObject newObj = Instantiate(prefabCharRight, parentCharRight);
            UI_Character uiCharacter = newObj.GetComponent<UI_Character>();
            uiCharacter.ChooseCharacterImg(characterNumber);
            uiCharacters[playerNumber] = uiCharacter;
            return uiCharacter;
        }

        public void StartCountdown() {
            animatorSemaforo.SetTrigger("Countdown");
        }
    }
}