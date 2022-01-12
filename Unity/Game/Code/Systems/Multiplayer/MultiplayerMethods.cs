using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JamCat.UI;

namespace JamCat.Multiplayer
{
    public class MultiplayerMethods : NetworkBehaviour 
    {
        public static MultiplayerMethods instance;
        public static MultiplayerMethods Get() { return instance; }

        public void OnStart() {
            instance = this;
        }

        // On Connected

        /*
        [ServerRpc]
        public void OnConnectServerRpc(ulong clientID) {
            if (IsServer == false) 
                return;

            ClientRpcParams clientRpcParams = new ClientRpcParams {
                Send = new ClientRpcSendParams {
                    TargetClientIds = new ulong[] { clientID }
                }
            };

            OnConnectClientRpc(curPlayers, maxPlayers);
        }
        */
        
        // ------------------------- Lobby
        [ServerRpc]
        public void OnConnectServerRpc() {
            if (IsServer == false) 
                return;

            int curPlayers = SysMultiplayer.Get().curPlayers;
            int maxPlayers = SysMultiplayer.Get().maxPlayers;
            OnConnectClientRpc(curPlayers, maxPlayers);
        }

        [ClientRpc]
        public void OnConnectClientRpc(int curPlayers, int maxPlayers) {
            SysMultiplayer.Get().curPlayers = curPlayers;
            SysMultiplayer.Get().maxPlayers = maxPlayers;
        }

        // ------------------------- Character Selection
        [ServerRpc]
        public void OnReadyServerRpc(ulong id, bool readyState) {
            if (IsServer == false) 
                return;

            SysMultiplayer.Get().PlayerReady(id, readyState);
        }

        [ClientRpc]
        public void OnReadyClientRpc(int playersReady) {
            SysMultiplayer.Get().clientPlayersReady = playersReady;
        }

        // ------------------------- Character Selection
        [ClientRpc]
        public void CallCaracterSelectionClientRpc() {
            SysMultiplayer.Get().serverPlayersReady = new Dictionary<ulong, bool>();
            SysMultiplayer.Get().clientPlayersReady = 0;
            Window_Lobby.Get().CloseWindow(0.3f, 0);
            Window_CharacterSelection.Get().OpenWindow(0.3f, 0.3f);
        }

        [ClientRpc]
        public void StartGameClientRpc() {
            GeneralMethods.StartGame();
        }

        [ClientRpc]
        public void OnConnectToOneClientRpc(int maxPlayers, ClientRpcParams clientRpcParams = default) {
            if (IsOwner == true)
                return;

            // this.maxPlayers = maxPlayers;
        }
    }
}