using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamCat.Players
{
    public class SysPlayer : Sys {
        
        // Instance
        public static SysPlayer instance;
        public static SysPlayer Get() { return instance; }

        // Variables
        public ulong localPlayerID;
        public GameObject localPlayerObj;
        public Player localPlayer;

        public List<Player> onlinePlayers;

        public GameObject prefabLocalPlayer;
        public List<Player> localPlayers;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            onlinePlayers = new List<Player>();
            localPlayers = new List<Player>();
        }

        protected override void OnStart() {

        }
    
        protected override void OnUpdate() {
            if (Data.Get().gameLogic.in_game == false)
                return;

            // localPlayer.OnUpdate();
            if (Data.Get().gameData.localMode == false) { 
                
                for (int i = 0; i < onlinePlayers.Count; i++)
                    onlinePlayers[i].OnUpdate();
                if (Input.GetKeyDown(KeyCode.P))
                    SysMultiplayer.Get().multiplayerMethods.RemoveLifeServerRpc(localPlayerID);

            } else {

                for (int i = 0; i < localPlayers.Count; i++)
                    localPlayers[i].OnUpdate();

            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (Data.Get().gameLogic.is_paused == false) {
                    GeneralMethods.PauseGame(true);
                } else {
                    GeneralMethods.PauseGame(false);
                }
            }
        }

        public void Restart() {
            if (Data.Get().gameData.localMode == false) {
                if (localPlayerObj == null)
                    return;

                localPlayer = localPlayerObj.GetComponent<Player>();
                localPlayer.Restart();
            } else {
                for (int i = 0; i < localPlayers.Count; i++) 
                    Destroy(localPlayers[i].gameObject);
                localPlayers = new List<Player>();
                
                if (Data.Get().gameLogic.in_game == true) {
                    for (int i = 0; i < Data.Get().gameData.charactersSelected.Length; i++) {
                        GameObject newPlayer = Instantiate(prefabLocalPlayer, transform);
                        Player player = newPlayer.GetComponent<Player>();
                        player.ChooseCharacter(Data.Get().gameData.charactersSelected[i]);
                        player.playerID = i;
                        localPlayers.Add(player);
                    }
                }
            }
        }

        public void UpdateOnlinePlayers(ulong[] onlinePlayersIDs, int[] characters) {
            for (int i = 0; i < onlinePlayersIDs.Length; i++) {
                getPlayer(onlinePlayersIDs[i]).ChooseCharacter(characters[i]);
            }
        }

        public Player getPlayer(ulong id) {
            for (int i = 0; i < onlinePlayers.Count; i++)
                if (onlinePlayers[i].getNetworkObject().OwnerClientId == id)
                    return onlinePlayers[i];
            return null;
        }

        public Player getLocalPlayer(int id) {
            return localPlayers[id];
        }
    }
}