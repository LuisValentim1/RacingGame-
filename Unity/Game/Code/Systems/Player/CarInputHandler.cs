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
            Vector2 inputVector = Vector2.zero;
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            topDownCarController.SetInputVector(inputVector);

            if(Input.GetButtonDown("Interaction")){
                player.InteractJump();
            }

            if(Input.GetButton("Interaction")){
                player.InteractStripe();
            }
        } 

        public void Restart() {
            topDownCarController.SetInputVector(Vector2.zero);
        }
    }
}