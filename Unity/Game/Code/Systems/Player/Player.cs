using System;
using System.Collections;
using UnityEngine;
using JamCat.Cameras;
using JamCat.Characters;
using JamCat.Map;
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


        // Variables -> Sync
        public int inModule = 0;
        

        // Methods ->
        private void Awake() {
            OnAwake();
        }

        private void Start() {
            OnStart();
        }

        // Methods -> Standard
        public void OnAwake() {
            // Auto Configuration
            networkObject = GetComponent<NetworkObject>();
            topDownCarController = GetComponent<TopDownCarController>();
            carInputHandler = GetComponent<CarInputHandler>();
            wheelTrailRenderedHandlers = GetComponentsInChildren<WheelTrailRenderedHandler>();
            SysPlayer.Get().onlinePlayers.Add(this);

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
            character.OnUpdate();
            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++)
                wheelTrailRenderedHandlers[i].OnUpdate();

            if (character.isOut == true) {
                carInputHandler.Restart();
                return;
            }

            carInputHandler.UpdateCar();
            if (Input.GetButtonDown("Interaction")) {
                InteractJump();
                InteractStripe();
            }

            if (Input.GetKeyDown(KeyCode.K)) {
                character.UseAbilityBasic();
            }
            
            if (Input.GetKeyDown(KeyCode.L)) {
                character.UseAbilityUlti();
            }
        }

        private void OnDestroy() {
            SysPlayer.Get().onlinePlayers.Remove(this);
        }


        void OnTriggerEnter2D(Collider2D collider2d) {
            if(collider2d.CompareTag("Obstacle"))
                topDownCarController.HitObstacle(collider2d);

            if (networkObject.IsLocalPlayer == false)
                return;

            if(collider2d.CompareTag("Jump")) 
                topDownCarController.TriggerJump();

            if(collider2d.CompareTag("Stripe"))
                stripeFlag = true;
            
            if(collider2d.CompareTag("Finish")) 
                GeneralMethods.CallFinish();

            if(collider2d.CompareTag("Module")) {
                Module module = collider2d.GetComponent<Module>();
                inModule = module.moduleNumber;
                SysMultiplayer.Get().multiplayerMethods.SetPlayerInModuleServerRpc(SysPlayer.Get().localPlayerID, inModule);
            }

            if(collider2d.GetComponent<ASlow>() != null) {
                // print("A");
                ASlow aSlow = collider2d.GetComponent<ASlow>();
                topDownCarController.ApplySlow(aSlow.slowIntensity, aSlow.maxAcceleration, aSlow.slowDuration);
            }
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
                character.AddMana(40f);
            }
        }


        // Methods -> Get
        public NetworkObject GetNetworkObject() { return networkObject; }
    }
}