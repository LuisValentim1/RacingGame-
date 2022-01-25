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

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            onlinePlayers = new List<Player>();
        }

        protected override void OnStart() {

        }
    
        protected override void OnUpdate() {
            if (Data.Get().gameLogic.in_game == false)
                return;

            // localPlayer.OnUpdate();
            for (int i = 0; i < onlinePlayers.Count; i++)
                onlinePlayers[i].OnUpdate();

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (Data.Get().gameLogic.is_paused == false) {
                    GeneralMethods.PauseGame(true);
                } else {
                    GeneralMethods.PauseGame(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.P))
                SysMultiplayer.Get().multiplayerMethods.RemoveLifeServerRpc(localPlayerID);
        }

        public void Restart() {
            if (localPlayerObj == null)
                return;

            localPlayer = localPlayerObj.GetComponent<Player>();
            localPlayer.Restart();
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
    }
}