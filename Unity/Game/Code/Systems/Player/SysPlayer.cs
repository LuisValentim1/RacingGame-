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
        [Header("Configuration")]
        public SysPlayerServer sysPlayerServer;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            sysPlayerServer.OnAwake();
        }

        protected override void OnStart() {
            
        }
    
        protected override void OnUpdate() {
            sysPlayerServer.OnUpdate();

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (Data.Get().gameLogic.is_paused == false) {
                    GeneralMethods.PauseGame(true);
                } else {
                    GeneralMethods.PauseGame(false);
                }
            }
        }

        public void Restart() {
            sysPlayerServer.OnRestart();
        }
    }
}