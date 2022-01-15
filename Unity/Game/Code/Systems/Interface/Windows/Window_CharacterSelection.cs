using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using JamCat.Players;

namespace JamCat.UI 
{
    public class Window_CharacterSelection : Window
    {
        // Variables -> Instance
        public static Window_CharacterSelection instance;
        public static Window_CharacterSelection Get() { return instance; }

        // Variables
        public UI_ToggleGroup toggleGroup;
        public CanvasGroup toggleGroupBlocker;
        public UI_Toggle toggleReady;
        public Text textPlayersReady;

        int preCharacter = -1;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnUpdateWindow() {
            textPlayersReady.text = "Players Ready: " + SysMultiplayer.Get().clientPlayersReady + "/" + SysMultiplayer.Get().curPlayers;

            if (toggleGroup.toggleActivated == null) {
                toggleReady.setServerEnabled(false);
            } else {
                toggleReady.setServerEnabled(true);
            }

            if (SysMultiplayer.Get().networkManager.IsServer == true) {
                if (SysMultiplayer.Get().clientPlayersReady == SysMultiplayer.Get().curPlayers) {
                    SysMultiplayer.Get().multiplayerMethods.StartGameClientRpc();
                }
            }
        }

        protected override void OnOpenWindow() {
            Data.Get().gameData.characterSelected = -1;
            toggleGroup.setServerEnabledAll(true);
            toggleGroup.ActivateAll(false);
            toggleReady.Activate(false);
        }

        protected override void OnCloseWindow() {

        }

        // Methods -> Public
        public void UpdateCharactersAvaiable(int[] characters, bool[] ready) {
          /*
            for (int i = 0; i < characters.Length; i++) {
                print("c: " + characters[i] + ",  r: " + ready[i]);
                if (characters[i] == Data.Get().gameData.characterSelected && toggleReady.activated == true) {
                    
                } else {
                    toggleGroup.setServerEnabled(characters[i], !ready[i]);
                }
            }
            */
            if (toggleReady.activated == true) {
                for (int i = 0; i < toggleGroup.toggles.Length; i++) {
                    if (Data.Get().gameData.characterSelected == i) {
                        toggleGroup.setServerEnabled(i, true);
                    } else {
                        toggleGroup.setServerEnabled(i, false);
                    }
                } 
            } else {
                for (int i = 0; i < toggleGroup.toggles.Length; i++)
                    toggleGroup.setServerEnabled(i, true);

                for (int i = 0; i < characters.Length; i++) {
                    toggleGroup.setServerEnabled(characters[i], !ready[i]);
                   
                    if (preCharacter == characters[i] && ready[i] == true) {
                        preCharacter = -1;
                        toggleGroup.toggleActivated.Activate(false);
                    }
                } 
            }
        }
        
        public void ToggleReady() {
            if (toggleReady.activated == true)
                Data.Get().gameData.characterSelected = preCharacter;
            else 
                Data.Get().gameData.characterSelected = -1;

            SysMultiplayer.Get().multiplayerMethods.OnReadyServerRpc(
                SysPlayer.Get().localPlayerID,
                toggleReady.activated,
                Data.Get().gameData.characterSelected
            );
        }

        public void ButtonSelectCharacter(int number) {
            preCharacter = number;
        }
        
        public void ButtonBack() {
            preCharacter = -1;
            Data.Get().gameData.characterSelected = -1;
            SysMultiplayer.Get().Disconnect();
            CloseWindow(0.2f, 0);
            Window_Lobby.Get().OpenWindow(0.2f, 0.2f);
        }
    }
}