using System;
using System.Collections;
using UnityEngine;


namespace CatJam.Players
{
    public class SysPlayer : Sys {
        
        // Instance
        public static SysPlayer instance;
        public static SysPlayer Get() { return instance; }

        // Variables
        public Player player;
        public Player[] onlinePlayers;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            player.OnAwake();
        }

        protected override void OnStart() {
            player.OnStart();
        }
    
        protected override void OnUpdate() {
            player.OnUpdate();

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (Data.Get().gameLogic.is_paused == false) {
                    GeneralMethods.PauseGame(true);
                } else {
                    GeneralMethods.PauseGame(false);
                }
            }
        }

        public void RestartAllPlayers() {
            for (int i = 0; i < onlinePlayers.Length; i++)
                onlinePlayers[i].Restart();
        }

        public void RestartPlayer() {
            player.Restart();
        }
    }
}