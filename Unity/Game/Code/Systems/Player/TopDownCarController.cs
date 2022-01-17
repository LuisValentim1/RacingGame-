using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Map;
using JamCat.Characters;
using JamCat.Multiplayer;
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
        public float currentVelocity;
        public Vector2 engineForceVector;
        public float accelerationInput = 0;
        public float steeringInput = 0;
        public float rotationAngle = 0;

        float velocityVsUp = 0;
        bool isJumping = false;

        public System.Random r;
        public bool hasControl = true;

        //Components
        Rigidbody2D carRigidbody2D;
        Collider2D carCollider2D;


        // Methods -> Standard

        public void AwakeCar() {
            r = new System.Random(DateTime.Now.Second);
            player = GetComponent<Player>();
            carRigidbody2D = GetComponent<Rigidbody2D>();
            carCollider2D = GetComponentInChildren<Collider2D>();
            initialScale = carShadowRenderer.transform.localScale;
        }
        
        public void StartCar() {
            Restart();
        }

        public void UpdateCar() {
            if (Data.Get().gameLogic.in_game == false)
                return;

            if (player.getNetworkObject().IsLocalPlayer == false)
                return;
                
            MultiplayerMethods.Get().UpdateVelocityServerRpc(SysPlayer.Get().localPlayerID, getVelocity());
        }

        private void FixedUpdate() {
            if (player.getNetworkObject().IsLocalPlayer == false)
                return;

            currentVelocity = carRigidbody2D.velocity.magnitude;
            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();
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
        

        // Methods -> Physics

        void ApplyEngineForce() {
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

            engineForceVector = transform.up * accelerationInput * accelerationFactor;
            carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
        }

        void ApplySteering() {
            float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
            minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

            rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
            carRigidbody2D.MoveRotation(rotationAngle);
        }

        void KillOrthogonalVelocity() {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
            Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

            carRigidbody2D.velocity = forwardVelocity + rightVelocity*driftFactor;
        
            if (isSlowingDown == true)
                carRigidbody2D.AddForce(slowingDownByVector);
        }


        
        // Methods -> Getter and Setter
        
        public void setInputVector(Vector2 inputVector) {
            steeringInput = inputVector.x;
            accelerationInput = inputVector.y;
        }

        public Vector2 getInputVector() {
            return new Vector2(steeringInput, accelerationInput);
        }
        

        public void setVelocity(float value) {
            currentVelocity = value;
        }
        
        public float getVelocity() {
            return currentVelocity;
        }
        
        float getLateralVelocity() {
            return Vector2.Dot(transform.right, carRigidbody2D.velocity);
        }

        public bool isTireScreeching(out float lateralVelocity, out bool isBreaking) {
            lateralVelocity = getLateralVelocity();
            isBreaking = false;

            if (accelerationInput < 0 && velocityVsUp > 0)
            {
                isBreaking = true;
                return true;
            }

            if (Mathf.Abs(getLateralVelocity()) > 4.0f)
                return true;

            return false;
        }


        // Methods -> Jump
        
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

            player.jumpFlag = false;
            carSpriteRenderer.transform.localScale = Vector3.one;
            carShadowRenderer.transform.localPosition = Vector3.zero;
            carShadowRenderer.transform.localScale = initialScale;
            carCollider2D.enabled = true;
            isJumping = false;
            accelerationFactor = 50;
            StopAllCoroutines();
        }


        // Methods -> Oil
        
        public void TriggerOil(float time) {
            hasControl = false;
            StartCoroutine(RegainControl(time));
        }

        private IEnumerator RegainControl(float time) {
            yield return new WaitForSeconds(time);
            hasControl = true;
            yield return null;
        }
        
        public Vector2 randomizeInputVector(){
            Vector2 curInput = getInputVector();
            Vector2 randomizedInput = new Vector2((float)(r.NextDouble() * (curInput.x+0.005 - curInput.x-0.005) + curInput.x-0.005), (float)(r.NextDouble() * (curInput.y+0.005 - curInput.y-0.005) + curInput.y-0.005));
            return curInput;
        }
        



        // Methods -> Obstacle
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


        // Methods -> Slow
        bool isSlowingDown;
        Vector2 slowingDownByVector;

        public void ApplySlow(float slowIntensity, float maxAccerelation, float slowDuration) {
            StartCoroutine(IE_ApplySlow(slowIntensity, maxAccerelation, slowDuration));
            StartCoroutine(IE_ReduceVelocityBy(slowIntensity, 1));
        }

        IEnumerator IE_ApplySlow(float slowIntensity, float maxAcceleration, float slowDuration) {
            this.accelerationFactor = maxAcceleration;
            yield return new WaitForSeconds(slowDuration);
            this.accelerationFactor = 50;

            yield return null;
        }
        
        IEnumerator IE_ReduceVelocityBy(float slowIntensity, float slowDuration) {
           isSlowingDown = true;
            float timer = slowDuration;
            while(timer > 0) {
                timer -= Time.deltaTime;
                slowingDownByVector = carRigidbody2D.velocity * slowIntensity * -1;
                yield return null;
            }

            isSlowingDown = false;
            yield return null;
        }
        
        public void ReduceVelocityBy(float value) {
            carRigidbody2D.velocity /= value;
        }
    }
}