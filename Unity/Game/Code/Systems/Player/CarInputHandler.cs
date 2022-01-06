using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CatJam.Players 
{
    public class CarInputHandler : MonoBehaviour
    {
        // Variables
        TopDownCarController topDownCarController;

        // Methods -> Standard
        public void AwakeCar() {
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
                topDownCarController.InteractJump();
            }
            if(Input.GetButton("Interaction")){
                topDownCarController.InteractStripe();
            }
        } 
    }
}