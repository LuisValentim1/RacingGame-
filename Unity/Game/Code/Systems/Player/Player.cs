using System;
using System.Collections;
using UnityEngine;
using JamCat.Cameras;
using JamCat.Characters;
using Unity.Netcode;

namespace JamCat.Players 
{
    public class Player : MonoBehaviour 
    {
        // Variables -> Private
        public Character character;

        private NetworkObject networkObject;
        private TopDownCarController topDownCarController;
        private CarInputHandler carInputHandler;
        private WheelTrailRenderedHandler[] wheelTrailRenderedHandlers;

        public Boolean jumpFlag;
        public Boolean stripeFlag;

        public int inModule = 0;
        

        private void Awake() {
            OnAwake();
        }

        private void Start() {
            OnStart();

            /*
            print(NetworkManager.Singleton.LocalClientId);
            print(NetworkManager.Singleton.LocalClient);
            print(NetworkManager.Singleton.LocalClient.PlayerObject);
            print(NetworkManager.Singleton.LocalClient.PlayerObject.gameObject);
            
            if (NetworkManager.Singleton.LocalClient.PlayerObject.gameObject == gameObject) {
                print("Awooo");
                local = this;
            }
            */
        }

        // Methods -> Standard
        public void OnAwake() {
            // Auto Configuration
            networkObject = GetComponent<NetworkObject>();
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
            if (networkObject.IsLocalPlayer == false)
                return;

            topDownCarController.StartCar();
            carInputHandler.StartCar();
            character.OnStart();

            SysCamera.Get().SetPlayerTarget(transform);
        }

        public void OnUpdate() {
            if (networkObject.IsLocalPlayer == false)
                return;

            topDownCarController.UpdateCar();
            carInputHandler.UpdateCar();
            character.OnUpdate();
            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++)
                wheelTrailRenderedHandlers[i].OnUpdate();

            if (Input.GetButtonDown("Interaction")) {
                InteractJump();
                InteractStripe();
            }
        }


        void OnTriggerEnter2D(Collider2D collider2d) {
            if (networkObject.IsLocalPlayer == false)
                return;

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
            if (networkObject.IsLocalPlayer == false)
                return;

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


        // Methods -> Get
        public NetworkObject GetNetworkObject() { return networkObject; }
    }
}