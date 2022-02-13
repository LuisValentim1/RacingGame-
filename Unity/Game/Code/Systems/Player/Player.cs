using System;
using System.Collections;
using UnityEngine;
using JamCat.Cameras;
using JamCat.Characters;
using JamCat.Multiplayer;
using JamCat.Map;
using JamCat.UI;
using Unity.Netcode;

namespace JamCat.Players 
{
    public class Player : MonoBehaviour 
    {
        // Variables

        [Header("Configurable")]
        public SpriteRenderer spriteRenderer;
        public GraphicChanger graphicChanger;

        [Header("Run-Time")]
        [SerializeField] private Character character;
        public Boolean jumpFlag;
        public Boolean stripeFlag;

        private NetworkObject networkObject;
        private TopDownCarController topDownCarController;
        private CarInputHandler carInputHandler;
        private WheelTrailRenderedHandler[] wheelTrailRenderedHandlers;
    
        public int playerID;


        [Header("Synchronized")]
        public int inModule = 0;
        public float dist = 0.0f;
    
        

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

            // Awake the methods
            topDownCarController.AwakeCar();
            carInputHandler.AwakeCar();
            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++) 
                wheelTrailRenderedHandlers[i].OnAwake();
        }

        public void OnStart() {
            if (Data.Get().gameData.localMode == false)
                if (networkObject.IsLocalPlayer == false)
                    return;

            topDownCarController.StartCar();
            carInputHandler.StartCar();

            if (Data.Get().gameData.localMode == false)
                SysCamera.Get().SetPlayerTarget(transform);
        }

        public void OnUpdate() {
            
            if (Data.Get().gameLogic.countdown > 0)
                return;
                
            if (character != null)
                character.OnUpdate();

            for (int i = 0; i < wheelTrailRenderedHandlers.Length; i++)
                wheelTrailRenderedHandlers[i].OnUpdate();

            graphicChanger.OnUpdate();

            if (Data.Get().gameData.localMode == false)
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
            // Para ficar sync em multiplayer
            if(collider2d.CompareTag("Obstacle")) {
                topDownCarController.HitObstacle(collider2d);
                character.OnHit();
            }

            if(collider2d.GetComponent<ASlow>() != null) {
                ASlow aSlow = collider2d.GetComponent<ASlow>();
                character.OnHit();
            }

            if(collider2d.GetComponent<AProject>() != null) {
                AProject aProject = collider2d.GetComponent<AProject>();
                character.OnHit();
            }

            if(collider2d.GetComponent<AOil>() != null) {
                AOil elementOil = collider2d.GetComponent<AOil>();
                elementOil.HitTarget("Player");
                character.OnHit();
            }

            if(collider2d.GetComponent<ALife>() != null) {
                ALife elementLife = collider2d.GetComponent<ALife>();
                character.OnHit();
            }

            if(collider2d.GetComponent<ElementOil>() != null) {
                ElementOil elementOil = collider2d.GetComponent<ElementOil>();
                character.OnHit();
            }



            // Se é Local Player
            if (Data.Get().gameData.localMode == false)
                if (networkObject.IsLocalPlayer == false)
                    return;

            if(collider2d.CompareTag("Jump")) 
                topDownCarController.TriggerJump();

            if(collider2d.CompareTag("Stripe"))
                stripeFlag = true;
            
            if(collider2d.CompareTag("Finish"))
                if (Data.Get().gameLogic.gameFinished == false)
                    GeneralMethods.CallFinish(character.characterNumber);

            if(collider2d.CompareTag("Module")) {
                Module module = collider2d.GetComponent<Module>();
                inModule = module.moduleNumber;

                if (Data.Get().gameData.localMode == false)
                    SysMultiplayer.Get().multiplayerMethods.SetPlayerInModuleServerRpc(SysPlayer.Get().localPlayerID, inModule);
                else {
                    GeneratorServer.Get().SetPlayerInModule_LocalMode(inModule);
                }

                SysPlayer.Get().OnEnterModule();
            }

            if(collider2d.GetComponent<ASlow>() != null) {
                // print("A");
                ASlow aSlow = collider2d.GetComponent<ASlow>();
                topDownCarController.ApplySlow(aSlow.slowIntensity, aSlow.maxAcceleration, aSlow.slowDuration);
            }

            if(collider2d.GetComponent<AProject>() != null) {
                AProject aProject = collider2d.GetComponent<AProject>();
                topDownCarController.ApplyProjectionForce(aProject.duration, aProject.transform.right, aProject.force);
            }

            if(collider2d.GetComponent<AOil>() != null) {
                AOil elementOil = collider2d.GetComponent<AOil>();
                topDownCarController.TriggerOil(elementOil.timerEffect);
            }

            if(collider2d.GetComponent<ALife>() != null) {
                ALife elementLife = collider2d.GetComponent<ALife>();
                character.RemoveLife(elementLife.removeLifes);
            }

            if(collider2d.GetComponent<ElementOil>() != null) {
                ElementOil elementOil = collider2d.GetComponent<ElementOil>();
                topDownCarController.TriggerOil(elementOil.timerEffect);
            }
        }
        
        private void OnTriggerExit2D(Collider2D collider2d) {
            if (Data.Get().gameData.localMode == false)
                if (networkObject.IsLocalPlayer == false)
                    return;

            if(collider2d.CompareTag("Stripe"))
                stripeFlag = false;
        }


        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.tag == "Element") {
                if (other.gameObject.GetComponent<ElementBarrel>() != null) {
                    other.gameObject.GetComponent<ElementBarrel>().StartAnimation();
                }
            }
                
            if (other.gameObject.GetComponent<ElementTree>() != null) {
                other.gameObject.GetComponent<Animator>().SetBool("hit", true);
            }
            
            if (Data.Get().gameData.localMode == false)
                if (networkObject.IsLocalPlayer == false)
                    return;

            if(other.gameObject.CompareTag("Player")) {
                if(topDownCarController.dashActivated == true) {
                    
                    if (Data.Get().gameData.localMode == false) {
                        if (SysPlayer.Get().localPlayer == this) {
                            ulong playerID = other.gameObject.GetComponent<NetworkObject>().OwnerClientId;
                            GetComponent<MultiplayerMethods>().RemoveLifeServerRpc(playerID);
                        }
                    } else {
                        other.gameObject.GetComponent<Player>().getCharacter().RemoveLife();
                    }

                }
            }
        }


        public void Restart() {
            ChooseCharacter(Data.Get().gameData.charactersSelected[playerID]);
            topDownCarController.Restart();
            character.Restart();
            graphicChanger.Restart(this);
        }

        public void ChooseCharacter(int characterNumber) {
            if (characterNumber < 0)
                return;

            character = GetComponentsInChildren<Character>()[characterNumber];
            character.OnAwake(this);
            character.OnStart();
            character.characterNumber = characterNumber;

            if (SysPlayer.Get().playersCreated == 0)
                character.setUICharacter(Window_HUD.Get().AddCharacterLeft(playerID, characterNumber));
            else
                character.setUICharacter(Window_HUD.Get().AddCharacterRight(playerID, characterNumber));

            SysPlayer.Get().playersCreated++;
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