using System;
using System.Collections;
using UnityEngine;


namespace CatJam.Player 
{
    public class SysPlayer : Sys {
        
        // Instance
        public static SysPlayer instance;
        public static SysPlayer Get() { return instance; }

        // Variables
        public CarInputHandler carInputHandler;
        public TopDownCarController topDownCarController;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            carInputHandler.AwakeCar();
            topDownCarController.AwakeCar();
        }

        protected override void OnStart() {
            carInputHandler.StartCar();
            topDownCarController.StartCar();
        }
    
        protected override void OnUpdate() {
            carInputHandler.UpdateCar();
            topDownCarController.UpdateCar();

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (Data.Get().gameLogic.is_paused == false) {
                    GeneralMethods.PauseGame(true);
                } else {
                    GeneralMethods.PauseGame(false);
                }
            }
        }


        public void RestartPlayer() {
            topDownCarController.Restart();
        }
    }
}