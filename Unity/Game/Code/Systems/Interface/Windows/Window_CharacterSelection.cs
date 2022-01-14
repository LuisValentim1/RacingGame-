using System.Collections;
using System.Collections.Generic;
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

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnUpdateWindow() {
            if (Data.Get().gameData.characterSelected < 0)
                toggleReady.SetInteractable(false);

            textPlayersReady.text = "Players Ready: " + SysMultiplayer.Get().clientPlayersReady + "/" + SysMultiplayer.Get().curPlayers;
       
            if (SysMultiplayer.Get().networkManager.IsServer == true) {
                if (SysMultiplayer.Get().clientPlayersReady == SysMultiplayer.Get().curPlayers) {
                    SysMultiplayer.Get().multiplayerMethods.StartGameClientRpc();
                }
            }
        }

        protected override void OnOpenWindow() {
            toggleGroup.DeactivateAll();
            Data.Get().gameData.characterSelected = -1;
            toggleReady.Activate(false);
        }

        protected override void OnCloseWindow() {

        }

        // Methods -> Public
        
        public void ToggleReady() {
            SysMultiplayer.Get().multiplayerMethods.OnReadyServerRpc(
                SysPlayer.Get().localPlayerID,
                toggleReady.activated,
                Data.Get().gameData.characterSelected
            );
        }
        
        public void ButtonPlay() {
            if (Data.Get().gameData.characterSelected < 0)
                return;

            CloseWindow(0.2f, 0);
            Window_HUD.Get().OpenWindow(0.2f, 0.2f);
            GeneralMethods.StartGame();
        }

        public void ButtonSelectCharacter(int number) {
            Data.Get().gameData.characterSelected = number;
            toggleReady.SetInteractable(true);
        }
        
        public void ButtonBack() {
            Data.Get().gameData.characterSelected = -1;
            SysMultiplayer.Get().Disconnect();
            CloseWindow(0.2f, 0);
            Window_Lobby.Get().OpenWindow(0.2f, 0.2f);
        }
    }
}