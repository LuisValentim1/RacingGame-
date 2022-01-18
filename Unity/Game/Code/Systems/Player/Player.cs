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
        // Variables

        [Header("Configurable")]
        public SpriteRenderer spriteRenderer;

        [Header("Run-Time")]
        [SerializeField] private Character character;
        public Boolean jumpFlag;
        public Boolean stripeFlag;

        private NetworkObject networkObject;
        private TopDownCarController topDownCarController;
        private CarInputHandler carInputHandler;
        private WheelTrailRenderedHandler[] wheelTrailRenderedHandlers;


        [Header("Synchronized")]
        public int inModule = 0;
        

        // Methods -> Standard

        private void Awake() {
            OnAwake();
        }

        private void Start() {
            OnStart();
        }

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
            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++) 
                wheelTrailRenderedHandlers[i].OnAwake();
        }

        public void OnStart() {
            if (networkObject.IsLocalPlayer == false)
                return;

            topDownCarController.StartCar();
            carInputHandler.StartCar();
            SysCamera.Get().SetPlayerTarget(transform);
        }

        public void OnUpdate() {
            if (character != null)
                character.OnUpdate();

            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++)
                wheelTrailRenderedHandlers[i].OnUpdate();

            if (networkObject.IsLocalPlayer == false)
                return;

            // Physics
            topDownCarController.UpdateCar();
            if (character.isOut == true) {
                carInputHandler.Restart();
                return;
            }

            // Controls
            carInputHandler.UpdateCar();
        }

        private void OnDestroy() {
            SysPlayer.Get().onlinePlayers.Remove(this);
        }


        void OnTriggerEnter2D(Collider2D collider2d) {
            if(collider2d.CompareTag("Obstacle")) {
                topDownCarController.HitObstacle(collider2d);
                character.OnHit();
            }

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
                character.OnHit();
            }

            if(collider2d.GetComponent<AProject>() != null) {
                AProject aProject = collider2d.GetComponent<AProject>();
                topDownCarController.ApplyProjectionForce(aProject.duration, aProject.transform.right, aProject.force);
                character.OnHit();
            }

            if(collider2d.GetComponent<AOil>() != null) {
                AOil elementOil = collider2d.GetComponent<AOil>();
                topDownCarController.TriggerOil(elementOil.timerEffect);
                character.OnHit();
            }

            if(collider2d.GetComponent<ElementOil>() != null) {
                // print("A");
                ElementOil elementOil = collider2d.GetComponent<ElementOil>();
                topDownCarController.TriggerOil(elementOil.timerEffect);
                character.OnHit();
            }
        }
        
        private void OnTriggerExit2D(Collider2D collider2d) {
            if (networkObject.IsLocalPlayer == false)
                return;

            if(collider2d.CompareTag("Stripe"))
                stripeFlag = false;
        }

        public void Restart() {
            AutoChooseCharacter();
            topDownCarController.Restart();
            character.Restart();
        }

        public void ChooseCharacter(int number) {
            if (number < 0)
                return;

            character = GetComponentsInChildren<Character>()[number];
            character.OnAwake(this);
            character.OnStart();
        }

        public void AutoChooseCharacter() {
            int number = Data.Get().gameData.characterSelected;
            if (number < 0)
                return;

            character = GetComponentsInChildren<Character>()[number];
            character.OnAwake(this);
            character.OnStart();
        }


        // Interactions
        public void InteractJump(){
            if(jumpFlag) {
                character.AddMana(5);
                topDownCarController.JumpBoost();
                jumpFlag = false;
            }
        }

        public void InteractStripe(){
            if(stripeFlag) {
                character.AddManaByTime(40f);
            }
        }

        // Methods -> Get
        public Character getCharacter() { return character; }
        public TopDownCarController getTopDownCarController() { return topDownCarController; }
        public NetworkObject getNetworkObject() { return networkObject; }
    }
}