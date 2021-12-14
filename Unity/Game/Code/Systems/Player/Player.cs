using System;
using System.Collections;
using UnityEngine;

namespace CatJam.Players 
{
    public class Player : MonoBehaviour 
    {
        
        // Variables -> Private
        public bool local;

        private TopDownCarController topDownCarController;
        private CarInputHandler carInputHandler;
        private WheelTrailRenderedHandler[] wheelTrailRenderedHandlers;

        // Methods -> Standard
        public void OnAwake() {
            // Auto Configuration
            topDownCarController = GetComponent<TopDownCarController>();
            carInputHandler = GetComponent<CarInputHandler>();
            wheelTrailRenderedHandlers = GetComponentsInChildren<WheelTrailRenderedHandler>();

            // Awake the methods
            topDownCarController.AwakeCar();
            carInputHandler.AwakeCar();
            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++) 
                wheelTrailRenderedHandlers[i].OnAwake();
        }

        public void OnStart() {
            if (local == false)
                return;

            topDownCarController.StartCar();
            carInputHandler.StartCar();
        }

        public void OnUpdate() {
            if (local == false)
                return;

            topDownCarController.UpdateCar();
            carInputHandler.UpdateCar();
            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++)
                wheelTrailRenderedHandlers[i].OnUpdate();
        }

        public void Restart() {
            topDownCarController.Restart();
        }
    }
}