using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JamCat.Map;
using JamCat.Players;
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
         [ClientRpc]
        public void CallCaracterSelectionClientRpc() {
            SysMultiplayer.Get().serverPlayersReady = new Dictionary<ulong, bool>();
            SysMultiplayer.Get().clientPlayersReady = 0;
            Window_Lobby.Get().CloseWindow(0.3f, 0);
            Window_CharacterSelection.Get().OpenWindow(0.3f, 0.3f);
        }

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

        [ServerRpc]
        public void StartGameServerRpc() {
            if (IsServer == false)
                return;

            StartGameClientRpc();
        }

        [ClientRpc]
        public void StartGameClientRpc() {
            GeneralMethods.StartGame();
        }
    



        // ------------------------- Map
        [ServerRpc]
        public void SetPlayerInModuleServerRpc(ulong playerID, int moduleID) {
            if (IsServer == false) 
                return;

            SetPlayerInModuleClientRpc(playerID, moduleID);

            if (GeneratorServer.Get().GetModuleCreated(moduleID).playerWasInside == true)
                return;

            GeneratorServer.Get().GetModuleCreated(moduleID).playerWasInside = true;
            GeneratorServer.Get().DeleteLastModule();     
            Module module = GeneratorServer.Get().GenerateModule();
            if (module != null)
                RequestNewModuleClientRpc(module.elementID, module.transform.position);
      
        }
        
        [ClientRpc]
        public void SetPlayerInModuleClientRpc(ulong playerID, int inModule) {
            if (IsServer == true)
                return;

            Player player = SysPlayer.Get().GetPlayer(playerID);
            player.inModule = inModule;
        }



        [ServerRpc]
        public void GenerateModuleServerRpc() {
             if (IsServer == false) 
                return;

            Module module = GeneratorServer.Get().GenerateModule();
            if (module != null)
                RequestNewModuleClientRpc(module.elementID, module.transform.position);
        }

        [ClientRpc]
        public void RequestNewModuleClientRpc(int elementID, Vector3 pos) {
            if (IsServer == true)
                return;
            
            // print("ElementID: " + elementID + ";  Pos: " + pos);
            GeneratorClient.Get().GenerateModule(elementID, pos);
        }
        

        [ServerRpc]
        public void DeleteLastModuleServerRpc() {
             if (IsServer == false) 
                return;
                
            GeneratorServer.Get().DeleteLastModule();
        }

        [ClientRpc]
        public void DestroyLastModuleClientRpc() {
            if (IsServer == true)
                return;

            print("Deleted");
            GeneratorClient.Get().DeleteLastModule();
        }


        // ------------------------- Player -> Life
        [ServerRpc]
        public void RemoveLifeServerRpc(ulong playerID) {
            if (IsServer == false)
                return;

            RemoveLifeClientRpc(playerID);
        }

        [ClientRpc]
        public void RemoveLifeClientRpc(ulong playerID) {
             SysPlayer.Get().GetPlayer(playerID).character.RemoveLife();
        }











        // ------------------------- Test
        [ServerRpc]
        public void RequestMapServerRpc() {

        }

        [ClientRpc]
        public void OnConnectToOneClientRpc(int maxPlayers, ClientRpcParams clientRpcParams = default) {
            if (IsOwner == true)
                return;

            // this.maxPlayers = maxPlayers;
        }
    }
}