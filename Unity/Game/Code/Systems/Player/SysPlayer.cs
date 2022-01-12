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

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
        }

        protected override void OnStart() {

        }
    
        protected override void OnUpdate() {
            if (Data.Get().gameLogic.in_game == false)
                return;

            localPlayer.OnUpdate();

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (Data.Get().gameLogic.is_paused == false) {
                    GeneralMethods.PauseGame(true);
                } else {
                    GeneralMethods.PauseGame(false);
                }
            }

        }

        public void Restart() {
            localPlayer = localPlayerObj.GetComponent<Player>();
            localPlayer.Restart();
        }
    }
}