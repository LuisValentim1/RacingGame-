using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CatJam.Player 
{
    public class TopDownCarController : MonoBehaviour
    {
        // Variables -> Public
        [Header("Car settings")]
        public float driftFactor = 0.80f;
        public float accelerationFactor = 0.01f;
        public float turnFactor = 0.5f;
        public float maxSpeed = 20.0f;

        // Variables -> Private
        float accelerationInput = 0;
        float steeringInput = 0;

        float rotationAngle = 0;
        public float rotOffset = -90;

        float velocityVsUp = 0;

        //Components
        Rigidbody2D carRigidbody2D;


        // Methods -> Standard
        public void AwakeCar() {
            carRigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        public void StartCar() {
            
        }

        public void UpdateCar() {
            
        }

        // Methods -> Public
        private void FixedUpdate() {
            if (Data.Get().gameLogic.is_paused == true)
                return;

            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();
        }


        // Methods -> Private
        void ApplyEngineForce() {
            velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

            if(velocityVsUp > maxSpeed * 0.5f && accelerationInput > 0)
                return;

            if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
                return;

            if(carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
                return;

            if(accelerationInput == 0)
                carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
            else
                carRigidbody2D.drag = 0;

            Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
            carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
            // carRigidbody2D.AddTorque(1, ForceMode2D.Impulse);
        }

        void ApplySteering() {
            float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
            minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

            rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
            carRigidbody2D.MoveRotation(rotationAngle + rotOffset);
        }

        void KillOrthogonalVelocity() {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
            Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

            carRigidbody2D.velocity = forwardVelocity + rightVelocity*driftFactor;
        }

        public void SetInputVector(Vector2 inputVector) {
            steeringInput = inputVector.x;
            accelerationInput = inputVector.y;
        }
    }
}