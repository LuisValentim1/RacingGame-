using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;

namespace JamCat.UI 
{
    public class Window_Lobby : Window
    {
        // Variables -> Instance
        public static Window_Lobby instance;
        public static Window_Lobby Get() { return instance; }

        // Variables
        private int inScreen;
        public Window_Screen windowScreen1, windowScreen2;
        public UI_Slider uiSliderPlayersQuantity;

        public Text textPlayersInLobby;

        public UI_Button buttonCloseConnection, buttonStart;
        public InputField inputFieldIP, inputFieldPort;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnUpdateWindow() {
            if (SysMultiplayer.Get().clientConnectedToServer == true) {
                textPlayersInLobby.text = "Players in Lobby: " 
                    + SysMultiplayer.Get().curPlayers + "/" 
                    + SysMultiplayer.Get().maxPlayers;
                    
                    /*
                if (NetworkManager.Singleton.IsServer == true) {
                    textPlayersInLobby.text = "Players in Lobby: " 
                        + NetworkManager.Singleton.ConnectedClients.Count + "/" 
                        + SysMultiplayer.Get().maxPlayers;
                } else {
                    textPlayersInLobby.text = "Players in Lobby: " 
                        + NetworkManager.Singleton.ConnectedClients.Count + "/" 
                        + SysMultiplayer.Get().maxPlayers;
                }
                */
            } else {
                textPlayersInLobby.text = "No connection found...";
            }
        }

        protected override void OnOpenWindow() {
            windowScreen2.CloseWindow(0, 0);
            windowScreen1.OpenWindow(0, 0);
        }

        protected override void OnCloseWindow() {
            
        }

        // Methods -> Public
        public void ButtonHost() {
            SysMultiplayer.Get().maxPlayers = uiSliderPlayersQuantity.value;
            SysMultiplayer.Get().uNetTransport.MaxConnections = uiSliderPlayersQuantity.value;
            SysMultiplayer.Get().uNetTransport.ConnectAddress = inputFieldIP.text;
            SysMultiplayer.Get().uNetTransport.ConnectPort = int.Parse(inputFieldPort.text);

            SysMultiplayer.Get().StartHost();
            buttonCloseConnection.SetInterectable(true);
            buttonStart.SetInterectable(true);
            GoToScreenTwo();
        }

        public void ButtonJoin() {
            SysMultiplayer.Get().StartClient();
            buttonCloseConnection.SetInterectable(false);
            buttonStart.SetInterectable(false);
            GoToScreenTwo();
        }

        public void ButtonCloseConnection() {
            SysMultiplayer.Get().Disconnect();
            GoToScreenOne();
        }

        public void ButtonStart() {
            CloseWindow(0.3f, 0);
            SysMultiplayer.Get().multiplayerMethods.CallCaracterSelectionClientRpc();
        }

        private void GoToScreenOne() {
            windowScreen2.CloseWindow(0.3f, 0);
            windowScreen1.OpenWindow(0.3f, 0.3f);
            inScreen = 1;
        }

        private void GoToScreenTwo() {
            windowScreen1.CloseWindow(0.3f, 0);
            windowScreen2.OpenWindow(0.3f, 0.3f);
            inScreen = 2;
        }
        
        public void ButtonBack() {
            if (inScreen == 2) {
                ButtonCloseConnection();
            } else {
                CloseWindow(0.5f, 0);
                Window_MainMenu.Get().OpenWindow(0.5f, 0.5f);
            }
        }
    }
}