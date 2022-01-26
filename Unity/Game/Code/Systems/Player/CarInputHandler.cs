using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JamCat.Players 
{
    public class CarInputHandler : MonoBehaviour
    {
        // Variables
        Player player;
        TopDownCarController topDownCarController;

        // Methods -> Standard
        public void AwakeCar() {
            player = GetComponent<Player>();
            topDownCarController = GetComponent<TopDownCarController>();
        }

        public void StartCar() {
            
        }

        public void UpdateCar() {
            if (Data.Get().gameData.localMode == false) {

                if(topDownCarController.hasControl == true){
                    Vector2 inputVector = Vector2.zero;
                    inputVector.x = Input.GetAxis("Horizontal");
                    inputVector.y = Input.GetAxis("Vertical");
                    topDownCarController.setInputVector(inputVector);

                    player.InteractStripe();
                
                    if(Input.GetButtonDown("Interaction"))
                        player.InteractJump();
                    
                    if (Input.GetKeyDown(KeyCode.K)) 
                        player.getCharacter().UseAbilityBasic();
                    
                    if (Input.GetKeyDown(KeyCode.L)) 
                        player.getCharacter().UseAbilityUlti();
                }
                else{
                    Vector2 inputVector = Vector2.zero;
                    Vector2 curInput = topDownCarController.getInputVector();
                    Vector2 randomizedInput = topDownCarController.randomizeInputVector();
                    inputVector.x = randomizedInput.x;
                    inputVector.y = randomizedInput.y;
                    topDownCarController.setInputVector(inputVector);
                }

            } else {
                if (player.playerID == 0) 
                    PlayerOne();
                else
                    PlayerTwo();
            }
            
        } 



        public void PlayerOne () {
            if(topDownCarController.hasControl == true) {
                Vector2 inputVector = Vector2.zero;
                inputVector.x = Input.GetAxis("Horizontal");
                inputVector.y = Input.GetAxis("Vertical");
                topDownCarController.setInputVector(inputVector);

                player.InteractStripe();
            
                if(Input.GetButtonDown("Interaction"))
                    player.InteractJump();
                
                if (Input.GetKeyDown(KeyCode.D)) 
                    player.getCharacter().UseAbilityBasic();
                
                if (Input.GetKeyDown(KeyCode.F)) 
                    player.getCharacter().UseAbilityUlti();
            } else {
                Vector2 inputVector = Vector2.zero;
                Vector2 curInput = topDownCarController.getInputVector();
                Vector2 randomizedInput = topDownCarController.randomizeInputVector();
                inputVector.x = randomizedInput.x;
                inputVector.y = randomizedInput.y;
                topDownCarController.setInputVector(inputVector);
            }
        }

        public void PlayerTwo () {
            if(topDownCarController.hasControl == true) {
                Vector2 inputVector = Vector2.zero;
                inputVector.x = Input.GetAxis("HorizontalPlayer2");
                inputVector.y = Input.GetAxis("VerticalPlayer2");
                topDownCarController.setInputVector(inputVector);

                player.InteractStripe();
            
                if(Input.GetKeyDown(KeyCode.Keypad0))
                    player.InteractJump();
                
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                    player.getCharacter().UseAbilityBasic();
                
                if(Input.GetKeyDown(KeyCode.RightArrow))
                    player.getCharacter().UseAbilityUlti();
            } else {
                Vector2 inputVector = Vector2.zero;
                Vector2 curInput = topDownCarController.getInputVector();
                Vector2 randomizedInput = topDownCarController.randomizeInputVector();
                inputVector.x = randomizedInput.x;
                inputVector.y = randomizedInput.y;
                topDownCarController.setInputVector(inputVector);
            }
        }

        public void Restart() {
            topDownCarController.setInputVector(Vector2.zero);
        }
    }
}