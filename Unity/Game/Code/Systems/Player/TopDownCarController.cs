using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Map;
using JamCat.Characters;
using Unity.Netcode;

namespace JamCat.Players
{
    public class TopDownCarController : NetworkBehaviour
    {
        // Variables -> Public
        Player player;

        [Header("Car settings")]
        public float driftFactor = 0.80f;
        public float accelerationFactor = 0.01f;
        public float turnFactor = 0.5f;
        public float maxSpeed = 20.0f;
        public float rotOffset = -90;
        public float maxMana = 100;
        public float currentMana = 0;

        [Header("Sprites")]
        public SpriteRenderer carSpriteRenderer;
        public SpriteRenderer carShadowRenderer;

        [Header("Jumping")]
        private Vector2 initialScale;
        public AnimationCurve jumpCurve;

        [Header("Jump info")]
        public float jumpHeightScale = 1.0f;
        public float jumpPushScale = 1.0f; 


        // Variables -> Private
        [Header("Test")]
        public float accelerationInput = 0;
        public float steeringInput = 0;
        public float rotationAngle = 0;

        float velocityVsUp = 0;
        bool isJumping = false;

        //Components
        Rigidbody2D carRigidbody2D;
        Collider2D carCollider2D;

        public void AwakeCar() {
            player = GetComponent<Player>();
            carRigidbody2D = GetComponent<Rigidbody2D>();
            carCollider2D = GetComponentInChildren<Collider2D>();
            initialScale = carShadowRenderer.transform.localScale;
        }
        
        public void StartCar() {
            Restart();
        }

        public void Restart() {
            // Restart all the variables
            transform.position = new Vector3(0, 0, -5);
            steeringInput = 0;
            accelerationInput = 0;
            velocityVsUp = 0;
            carRigidbody2D.velocity = Vector2.zero;

            // Player starts with the initial rotation of the track
            rotationAngle = GeneratorServer.Get().GetInitialPlayerRotation();
        }

        public void UpdateCar() {
            
        }

        private void FixedUpdate() {
            //  if (Data.Get().gameLogic.in_game == false)
            //    return;

            //  if (Data.Get().gameLogic.is_paused == true)
            //    return;

            if (player.GetNetworkObject().IsLocalPlayer == false)
                return;

            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();
        }



        // Methods
        
        public void ReduceVelocityBy(float value) {
            carRigidbody2D.velocity /= value;
        }
        
        void ApplyEngineForce()
        {
            velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

            if(velocityVsUp > maxSpeed * 0.5f && accelerationInput > 0){
                return;
            }

            if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0){
                return;
            }

            if(carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0){
                return;
            }
            
            if(accelerationInput == 0) {
                carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
            } else {
                carRigidbody2D.drag = 0;
            }

            Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
            carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
          //  AddForceServerRpc(engineForceVector);
        }

/*
        [ServerRpc]
        void AddForceServerRpc(Vector2 engineForceVector) {

        }
*/
        void ApplySteering()
        {
            float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
            minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

            rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
            carRigidbody2D.MoveRotation(rotationAngle);
        }

        void KillOrthogonalVelocity()
        {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
            Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

            carRigidbody2D.velocity = forwardVelocity + rightVelocity*driftFactor;
        }


        float GetLateralVelocity()
        {
            return Vector2.Dot(transform.right, carRigidbody2D.velocity);
        }

        public bool IsTireScreeching(out float lateralVelocity, out bool isBreaking)
        {
            lateralVelocity = GetLateralVelocity();
            isBreaking = false;

            if (accelerationInput < 0 && velocityVsUp > 0)
            {
                isBreaking = true;
                return true;
            }

            if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
                return true;

            return false;
        }

        public void TriggerJump() {
            player.jumpFlag = true; 
            if (!isJumping) {
                StartCoroutine(JumpFlagOver());
                StartCoroutine(JumpCo(jumpHeightScale, jumpPushScale));
            }
        }

        public void JumpBoost(){
            accelerationFactor = accelerationFactor * 2;
        }

        private IEnumerator JumpFlagOver() {
            yield return new WaitForSeconds(0.75f);
            player.jumpFlag = false;
            yield return null;
        }

        private IEnumerator JumpCo(float jumpHeightScale, float jumpPushScale) {
            isJumping = true;

            float jumpStartTime = Time.time;
            float jumpDuration = carRigidbody2D.velocity.magnitude * 0.05f;
            jumpHeightScale = jumpHeightScale * carRigidbody2D.velocity.magnitude * 0.05f;
            jumpHeightScale = Mathf.Clamp(jumpHeightScale, 0.0f, 1.0f);
            carCollider2D.enabled = false;

            while (isJumping)
            {

                float jumpCompletedPercentage = (Time.time - jumpStartTime) / jumpDuration;
                jumpCompletedPercentage = Mathf.Clamp01(jumpCompletedPercentage);

                carSpriteRenderer.transform.localScale = Vector3.one + Vector3.one  * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;
                carShadowRenderer.transform.localScale = carSpriteRenderer.transform.localScale * 0.75f;
                carShadowRenderer.transform.localPosition = new Vector3(1, -1, 0.0f) * 3 * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

                if (jumpCompletedPercentage == 1.0f)
                    break; 
                yield return null;
            }

            carSpriteRenderer.transform.localScale = Vector3.one;
            carShadowRenderer.transform.localPosition = Vector3.zero;
            carShadowRenderer.transform.localScale = initialScale;
            carCollider2D.enabled = true;
            isJumping = false;
            accelerationFactor = 50;
        }


        
       

         public void SetInputVector(Vector2 inputVector) {
            steeringInput = inputVector.x;
            accelerationInput = inputVector.y;
        }


        // Methods -> Interaction
        public void HitObstacle(Collider2D collider2D) {
            StartCoroutine(IE_HitObstacle(collider2D));
        }

        IEnumerator IE_HitObstacle(Collider2D collider2D) {
            Destroy(collider2D.gameObject);
            ElementObstacle elementObstacle = collider2D.GetComponent<ElementObstacle>();
            accelerationFactor = elementObstacle.GetAccelerationFactor();
            ReduceVelocityBy(elementObstacle.GetVelocityDivide());
            yield return new WaitForSeconds(2);
            accelerationFactor = 50;

            yield return null;
        }
    }
}