using System;
using System.Collections;
using UnityEngine;
using JamCat.Characters;

namespace JamCat.Players 
{
    public class Player : MonoBehaviour 
    {
        
        // Variables -> Private
        public bool local;

        public Character character;

        private TopDownCarController topDownCarController;
        private CarInputHandler carInputHandler;
        private WheelTrailRenderedHandler[] wheelTrailRenderedHandlers;

        public Boolean jumpFlag;
        public Boolean stripeFlag;

        
        // Methods -> Standard
        public void OnAwake() {
            // Auto Configuration
            topDownCarController = GetComponent<TopDownCarController>();
            carInputHandler = GetComponent<CarInputHandler>();
            wheelTrailRenderedHandlers = GetComponentsInChildren<WheelTrailRenderedHandler>();

            // Awake the methods
            topDownCarController.AwakeCar();
            carInputHandler.AwakeCar();
            character.OnAwake();
            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++) 
                wheelTrailRenderedHandlers[i].OnAwake();
        }

        public void OnStart() {
            if (local == false)
                return;

            topDownCarController.StartCar();
            carInputHandler.StartCar();
            character.OnStart();
        }

        public void OnUpdate() {
            if (local == false)
                return;

            topDownCarController.UpdateCar();
            carInputHandler.UpdateCar();
            character.OnUpdate();
            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++)
                wheelTrailRenderedHandlers[i].OnUpdate();


            if (jumpFlag)
                print("AWOOOOOO");

            if (Input.GetButtonDown("Interaction")) {
                InteractJump();
                InteractStripe();
            }
        }


        void OnTriggerEnter2D(Collider2D collider2d) {
            if(collider2d.CompareTag("Jump")) 
                topDownCarController.TriggerJump();

            if(collider2d.CompareTag("Stripe"))
                stripeFlag = true;

            if(collider2d.CompareTag("Obstacle"))
                topDownCarController.HitObstacle(collider2d);
            
            if(collider2d.CompareTag("Finish")) 
                GeneralMethods.CallFinish();
        }

        private void OnTriggerExit2D(Collider2D collider2d) {
            if(collider2d.CompareTag("Stripe"))
                stripeFlag = false;
        }
        

        public void Restart() {
            topDownCarController.Restart();
            character.Restart();
        }


        // Interactions
        public void InteractJump(){
            if(jumpFlag) {
                character.AddMana(5.0f);
                topDownCarController.JumpBoost();
            }
        }

        public void InteractStripe(){
            if(stripeFlag) {
                character.AddMana(0.2f);
            }
        }
    }
}