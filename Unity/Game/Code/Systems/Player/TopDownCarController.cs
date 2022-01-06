using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CatJam.Map;
using CatJam.Characters;

namespace CatJam.Players
{
    public class TopDownCarController : MonoBehaviour
    {

        public GameObject character;

        // Variables -> Public
        [Header("Car settings")]
        public float driftFactor = 0.80f;
        public float accelerationFactor = 0.01f;
        public float turnFactor = 0.5f;
        public float maxSpeed = 20.0f;
        public float rotOffset = -90;
        public float currentMana = 0;
        public Boolean jumpFlag;
        public Boolean stripeFlag;


        [Header("Sprites")]
        public SpriteRenderer carSpriteRenderer;
        public SpriteRenderer carShadowRenderer;

        [Header("Jumping")]
        public AnimationCurve jumpCurve;

        [Header("Jump info")]
        public float jumpHeightScale = 1.0f;
        public float jumpPushScale = 1.0f; 


        // Variables -> Private
        [Header("Test")]
        public float accelerationInput = 0;
        public float steeringInput = 0;
        public float rotationAngle = 0;
        public int inModule = 0;

        float velocityVsUp = 0;
        bool isJumping = false;

        //Components
        Rigidbody2D carRigidbody2D;
        Collider2D carCollider2D;
        WheelTrailRenderedHandler[] wheelTrailRenderedHandler;

        // Methods -> Standard
        public void AwakeCar() {
            carRigidbody2D = GetComponent<Rigidbody2D>();
            carCollider2D = GetComponentInChildren<Collider2D>();
            wheelTrailRenderedHandler = GetComponentsInChildren<WheelTrailRenderedHandler>();
            for (int i = 0; i < wheelTrailRenderedHandler.Length; i++)
                wheelTrailRenderedHandler[i].OnAwake();
        }
        
        public void StartCar() {

        }

        public void UpdateCar() {
            for (int i = 0; i < wheelTrailRenderedHandler.Length; i++)
                wheelTrailRenderedHandler[i].OnUpdate();
        }

        public void addMana(float manaValue){
            currentMana += manaValue;
            print(currentMana);
        }

        public void InteractJump(){
            if(jumpFlag){
                //character.currentMana(character.currentMana+10);
                addMana(5.0f);
                jumpBoost();
            }
        }

        public void InteractStripe(){
            if(stripeFlag){
                addMana(0.04f);
            } 
        }

        void OnTriggerExit2D(Collider2D collision){
            if(collision.CompareTag("Stripe")){
                stripeFlag = false;
            }       
        }
        
        void OnTriggerEnter2D(Collider2D collider2d) {
            if(collider2d.CompareTag("Jump")) {
                Jump(jumpHeightScale, jumpPushScale);
                jumpFlag = true; 
            }
            if(collider2d.CompareTag("Stripe")){
                stripeFlag = true;
            }
        }

        public void jumpBoost(){
            accelerationFactor = accelerationFactor * 2;
        }

        private void FixedUpdate() {
            if (Data.Get().gameLogic.is_paused == true)
                return;

            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();
            jumpFlag = false;
        }

        // Methods -> Private
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
        }

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

        public void SetInputVector(Vector2 inputVector)
        {
            steeringInput = inputVector.x;
            accelerationInput = inputVector.y;
        }

            
        public void Jump(float jumpHeightScale, float jumpPushScale)
        {
            if (!isJumping)
                StartCoroutine(JumpCo(jumpHeightScale, jumpPushScale));
        }

        private IEnumerator JumpCo(float jumpHeightScale, float jumpPushScale)
        {
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

                carSpriteRenderer.transform.localScale = Vector3.one + Vector3.one * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

                carShadowRenderer.transform.localScale = carSpriteRenderer.transform.localScale * 0.75f;

                carShadowRenderer.transform.localPosition = new Vector3(1, -1, 0.0f) * 3 * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

                if (jumpCompletedPercentage == 1.0f)
                    break; 
                yield return null;
            }

            carSpriteRenderer.transform.localScale = Vector3.one;

            carShadowRenderer.transform.localPosition = Vector3.zero;
            carShadowRenderer.transform.localScale = carSpriteRenderer.transform.localScale;

            carCollider2D.enabled = true;
            isJumping = false;
            accelerationFactor = 50;
        }

        public void Restart() {
            // Restart all the variables
            transform.position = new Vector3(0, 0, -5);
            steeringInput = 0;
            accelerationInput = 0;
            velocityVsUp = 0;
            carRigidbody2D.velocity = Vector2.zero;

            // Player starts with the initial rotation of the track
            rotationAngle = Generator.Get().GetInitialPlayerRotation();
        }
    }
}