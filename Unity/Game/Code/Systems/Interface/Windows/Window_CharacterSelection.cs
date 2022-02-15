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
        public UI_Toggle toggleReady;
        public Text textPlayersReady;
        public Text textPlayerChoosing;

        int preCharacter = -1;
        int localPlayersReady = 0;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnUpdateWindow() {

            if (toggleGroup.toggleActivated == null) {
                toggleReady.setServerEnabled(false);
            } else {
                toggleReady.setServerEnabled(true);
            }

            if (Data.Get().gameData.localMode == true) {
                if (Data.Get().gameData.trainingMode == true ) {
                    textPlayersReady.text = "Players Ready: " + localPlayersReady + "/" + 1;
                    if (localPlayersReady == 1) {
                        GeneralMethods.StartGame();
                    }
                } else {
                    textPlayersReady.text = "Players Ready: " + localPlayersReady + "/" + 2;
                    if (localPlayersReady == 2) {
                        GeneralMethods.StartGame();
                    }
                }
            } else {
                textPlayersReady.text = "Players Ready: " + SysMultiplayer.Get().clientPlayersReady + "/" + SysMultiplayer.Get().curPlayers;
                if (SysMultiplayer.Get().networkManager.IsServer == true) {
                    if (SysMultiplayer.Get().clientPlayersReady == SysMultiplayer.Get().curPlayers) {
                        SysMultiplayer.Get().multiplayerMethods.StartGameClientRpc();
                    }
                }
            }
        }

        protected override void OnOpenWindow() {
            localPlayersReady = 0;

            if (Data.Get().gameData.localMode == true) {
                if (Data.Get().gameData.trainingMode == true) {
                    Data.Get().gameData.charactersSelected = new int[1];
                } else {
                    Data.Get().gameData.charactersSelected = new int[2];
                }
            }

            for (int i = 0; i < Data.Get().gameData.charactersSelected.Length; i++)
                Data.Get().gameData.charactersSelected[i] = -1;

            Data.Get().gameData.characterSelected = -1;
            
            toggleGroup.setServerEnabledAll(true);
            toggleGroup.ActivateAll(false);
            toggleReady.Activate(false);

            textPlayerChoosing.text = "Player 1 choosing!";
        }

        protected override void OnCloseWindow() {

        }

        // Methods -> Public
        public void UpdateCharactersAvaiable(int[] characters, bool[] ready) {
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

        public void UpdateCharactersAvaiable_LocalMode() {
            for (int i = 0; i < toggleGroup.toggles.Length; i++)
                toggleGroup.setServerEnabled(i, true);
                
            if (Data.Get().gameData.localMode == true ) {
                if (Data.Get().gameData.trainingMode == true ) {
                    if (Data.Get().gameData.charactersSelected[0] > -1)
                        toggleGroup.setServerEnabled(Data.Get().gameData.charactersSelected[0], false);
                } else {
                    for (int y = 0; y < 2; y++)
                        if (Data.Get().gameData.charactersSelected[y] > -1)
                            toggleGroup.setServerEnabled(Data.Get().gameData.charactersSelected[y], false);
                }
            }
        }
        
        public void ToggleReady() {
            if (Data.Get().gameData.localMode == false) {
                if (toggleReady.activated == true)
                    Data.Get().gameData.characterSelected = preCharacter;
                else 
                    Data.Get().gameData.characterSelected = -1;

                SysMultiplayer.Get().multiplayerMethods.OnReadyServerRpc(
                    SysPlayer.Get().localPlayerID,
                    toggleReady.activated,
                    Data.Get().gameData.characterSelected
                );
            } else {
                Data.Get().gameData.charactersSelected[localPlayersReady] = preCharacter;
                Data.Get().gameData.characterSelected = preCharacter = -1;
                localPlayersReady++;
                UpdateCharactersAvaiable_LocalMode();
                if (localPlayersReady < Data.Get().gameData.charactersSelected.Count()) {
                    int nextPlayer = localPlayersReady + 1;
                    textPlayerChoosing.text = "Player " + nextPlayer + " choosing!";
                }
            }
        }

        public void ButtonSelectCharacter(int number) {
            preCharacter = number;
        }
        
        public void ButtonBack() {
            if (Data.Get().gameData.localMode == false) {
                SysMultiplayer.Get().Disconnect();
            } else {
                Data.Get().gameData.charactersSelected = new int[2];
            }

            preCharacter = -1;
            Data.Get().gameData.characterSelected = -1;
            CloseWindow(0.2f, 0);
            Window_Lobby.Get().OpenWindow(0.2f, 0.2f);
        }
    }
}