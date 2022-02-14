using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Cameras;
using JamCat.Map;

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

        public float curDist = 100000.0f;
        public int playersCreated = 0;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            onlinePlayers = new List<Player>();
        }

        protected override void OnStart() {

        }
    
        protected override void OnUpdate() {
            if (Data.Get().gameLogic.inGame == false)
                return;

            // localPlayer.OnUpdate();
            if (Data.Get().gameData.localMode == false) { 
                for (int i = 0; i < onlinePlayers.Count; i++)
                    onlinePlayers[i].OnUpdate();
                if (Input.GetKeyDown(KeyCode.P))
                    SysMultiplayer.Get().multiplayerMethods.RemoveLifeServerRpc(localPlayerID);
            } else {
                for (int i = 0; i < onlinePlayers.Count; i++)
                    onlinePlayers[i].OnUpdate();
            }
            
            if (Data.Get().gameLogic.gameFinished == false) {
                UpdateFirstCar();
                UpdateDeaths();

                if (Input.GetKeyDown(KeyCode.Escape)) {
                    if (Data.Get().gameLogic.isPaused == false) {
                        GeneralMethods.PauseGame(true);
                    } else {
                        GeneralMethods.PauseGame(false);
                    }
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
                for (int i = 0; i < onlinePlayers.Count; i++) 
                    Destroy(onlinePlayers[i].gameObject);

                onlinePlayers = new List<Player>();
                
                if (Data.Get().gameLogic.inGame == true) {
                    for (int i = 0; i < Data.Get().gameData.charactersSelected.Length; i++) {
                        GameObject newPlayer = Instantiate(prefabLocalPlayer, transform);
                        Player player = newPlayer.GetComponent<Player>();
                        player.playerID = i;
                        player.Restart();
                        onlinePlayers.Add(player);
                    }
                }
            }

            playersCreated = 0;
        }

        public void UpdateOnlinePlayers(ulong[] onlinePlayersIDs, int[] characters) {
            for (int i = 0; i < onlinePlayersIDs.Length; i++) {
                getPlayer(onlinePlayersIDs[i]).ChooseCharacter(characters[i]);
            }
        }


        public void OnEnterModule() {
            curDist = 1000000f;
        }

        public void UpdateFirstCar(){
            for (int i = 0; i < onlinePlayers.Count; i++) {
                if (onlinePlayers[i].inModule == getInFrontModule()) {
                    if (CheckCloser(onlinePlayers[i])) {
                        SysCamera.Get().SetPlayerTarget(onlinePlayers[i].transform);
                    }
                }
            }
        }

        bool deathCalled = false;
        public void UpdateDeaths() {
            if (Data.Get().gameLogic.countdown <= 0) {
                deathCalled = false;
                for(int i = 0; i < onlinePlayers.Count; i++) {
                    if(CheckDeath(onlinePlayers[i]) && onlinePlayers[i].getCharacter().isAlive() == true) {
                        onlinePlayers[i].getCharacter().RemoveLife();

                        if (getPlayersAlive().Length == 1) {
                            GeneralMethods.CallFinish(getPlayersAlive()[0].getCharacter().characterNumber);
                        } else {
                            if (deathCalled == false) {
                                RestartOnModule();
                                deathCalled = true;
                            }
                        }
                    }
                }
            }
        }

        public Player[] getPlayersAlive() {
            List<Player> value = new List<Player>();
            for (int i = 0; i < onlinePlayers.Count; i++)
                if (onlinePlayers[i].getCharacter().isAlive() == true)
                    value.Add(onlinePlayers[i]);
            return value.ToArray();
        }
        
        public bool CheckDeath(Player pl){
            if(IsObjectVisible(pl.GetComponentInChildren<SpriteRenderer>()))
                return false;
            else 
                return true;
        }

        public bool CheckCloser(Player pl){
            Module mod = GeneratorServer.Get().GetModuleCreated(pl.inModule);
            Vector3 mod_pos = mod.transform.position;
            Vector3 offset = new Vector3(mod.moduleConfiguration.to_direction.x * mod.moduleConfiguration.size, mod.moduleConfiguration.to_direction.y * mod.moduleConfiguration.size, 0);
            if (Vector3.Distance (mod_pos+offset, pl.transform.position) < curDist){
                curDist = Vector3.Distance(mod_pos + offset, pl.transform.position);
                return true;
            }
            return false;
        }

        public void RestartOnModule() {
            Module mod = GeneratorServer.Get().GetModuleCreated(getInFrontModule());
            Vector3 mod_pos = mod.transform.position;
            Vector3 offset = new Vector3(mod.moduleConfiguration.to_direction.x * mod.moduleConfiguration.size, mod.moduleConfiguration.to_direction.y * mod.moduleConfiguration.size,0);
            for(int i = 0; i < onlinePlayers.Count; i++){
                onlinePlayers[i].getTopDownCarController().Restart();
                onlinePlayers[i].transform.position = mod_pos - 1/2 * offset;
                // onlinePlayers[i].getTopDownCarController().rotationAngle = GeneratorServer.Get().GetInitialPlayerRotation();
            }

            GeneralMethods.StartCountdown(3);
        }

        public bool IsObjectVisible(Renderer renderer) {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(SysCamera.Get().getCurrentCamera()), renderer.bounds);
        }

        public Player getPlayer(ulong id) {
            for (int i = 0; i < onlinePlayers.Count; i++)
                if (onlinePlayers[i].getNetworkObject().OwnerClientId == id)
                    return onlinePlayers[i];
            return null;
        }

        public Player getLocalPlayer(int id) {
            return onlinePlayers[id];
        }

        public int getInFrontModule() {
            int value = -1;
            for (int i = 0; i < onlinePlayers.Count; i++)
                if (value < onlinePlayers[i].inModule)
                    value = onlinePlayers[i].inModule;
            return value;
        }
        
        public int getQuantPlayersInFrontModule() {
            int value = 0;
            for (int i = 0; i < onlinePlayers.Count; i++)
                if (onlinePlayers[i].inModule == getInFrontModule())
                    value++;
            return value;
        }
    }
}