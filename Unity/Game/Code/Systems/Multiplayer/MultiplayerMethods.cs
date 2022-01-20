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
            // SysMultiplayer.Get().serverPlayersReady = new Dictionary<ulong, bool>();
            SysMultiplayer.Get().clientPlayersReady = 0;
            Window_Lobby.Get().CloseWindow(0.3f, 0);
            Window_CharacterSelection.Get().OpenWindow(0.3f, 0.3f);
        }

        [ServerRpc]
        public void OnReadyServerRpc(ulong id, bool readyState, int character) {
            if (IsServer == false) 
                return;

            SysMultiplayer.Get().PlayerReady(id, readyState, character);
        }

        [ClientRpc]
        public void OnReadyClientRpc(ulong[] ids, int[] characters, bool[] ready) {
            print( ids.Length + " : " + characters.Length + " : " + ready.Length);
            int totalReady = 0;
            for (int i = 0; i < ready.Length; i++)
                if (ready[i] == true)
                    totalReady++;
            
            SysMultiplayer.Get().clientPlayersReady = totalReady;
            SysPlayer.Get().UpdateOnlinePlayers(ids, characters);
            Window_CharacterSelection.Get().UpdateCharactersAvaiable(characters, ready);
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
    

        // ------------------------- Player Enters Module
        [ServerRpc]
        public void SetPlayerInModuleServerRpc(ulong playerID, int moduleNumber) {
            if (IsServer == false) 
                return;

            SetPlayerInModuleClientRpc(playerID, moduleNumber);

            if (GeneratorServer.Get().GetModuleCreated(moduleNumber).playerWasInside == true)
                return;

            GeneratorServer.Get().GetModuleCreated(moduleNumber).playerWasInside = true;
            GeneratorServer.Get().DeleteLastModule();     
            Module module = GeneratorServer.Get().GenerateModule();
            if (module != null) {
                RequestNewModuleClientRpc(module.moduleID, module.transform.position);

                ModuleClient.SerializeElements serializeElements = module.GetComponent<ModuleClient>().GetSerializeElements();
                RequestElementsInModuleClientRpc(module.moduleNumber, serializeElements);
            }
        }
        
        [ClientRpc]
        public void SetPlayerInModuleClientRpc(ulong playerID, int inModule) {
            if (IsServer == true)
                return;

            Player player = SysPlayer.Get().getPlayer(playerID);
            player.inModule = inModule;
        }


        // -------------------------  Generate Module
        [ServerRpc]
        public void GenerateModuleServerRpc() {
             if (IsServer == false) 
                return;

            Module module = GeneratorServer.Get().GenerateModule();
            if (module != null) {
                RequestNewModuleClientRpc(module.moduleID, module.transform.position);
                ModuleClient.SerializeElements serializeElements = module.GetComponent<ModuleClient>().GetSerializeElements();
                RequestElementsInModuleClientRpc(module.moduleNumber, serializeElements);
            }
        }

        [ClientRpc]
        public void RequestNewModuleClientRpc(int elementID, Vector3 pos) {
            if (IsServer == true)
                return;
            
            GeneratorClient.Get().GenerateModule(elementID, pos);
        }
        
        [ClientRpc]
        public void RequestElementsInModuleClientRpc(int moduleNumber, ModuleClient.SerializeElements serializeElements) {
            if (IsServer == true)
                return;
            
            GeneratorClient.Get().GenerateElementsInModule(moduleNumber, serializeElements);
        }

        // -------------------------  Delete Last Module
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

            // print("Deleted");
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
             SysPlayer.Get().getPlayer(playerID).getCharacter().RemoveLife();
        }

        // ------------------------- Player -> Rigibody2D
        [ServerRpc]
        public void UpdateVelocityServerRpc(ulong playerID, float value) {
            if (IsServer == false)
                return;

            UpdateVelocityClientRpc(playerID, value);
        }

        [ClientRpc]
        public void UpdateVelocityClientRpc(ulong playerID, float value) {
            if (SysPlayer.Get().localPlayerID == playerID)
                return;

             SysPlayer.Get().getPlayer(playerID).getTopDownCarController().setVelocity(value);
        }


        // ------------------------- Abilities
        [ServerRpc]
        public void UseAbilityServerRpc(ulong playerID, int abilityID) {
            if (IsServer == false)
                return;

            UseAbilityClientRpc(playerID, abilityID);
        }

        [ClientRpc]
        public void UseAbilityClientRpc(ulong playerID, int abilityID) {
            if (SysPlayer.Get().localPlayerID == playerID)
                return;

            SysPlayer.Get().getPlayer(playerID).getCharacter().abilityGroup.GetAbility(abilityID).ReceiveInfoFromServer();
        }

        /*
        [ServerRpc]
        public void UseAbilityServerRpc(ulong playerID, int abilityID, string[] info) {
            if (IsServer == false)
                return;

            UseAbilityClientRpc(playerID, abilityID, info);
        }

        [ClientRpc]
        public void UseAbilityClientRpc(ulong playerID, int abilityID, string[] info) {
            if (SysPlayer.Get().localPlayerID == playerID)
                return;

            SysPlayer.Get().GetPlayer(playerID).getCharacter().abilityGroup.GetAbility(abilityID).ReceiveInfoFromServer(info);
        }
        */
    }
}