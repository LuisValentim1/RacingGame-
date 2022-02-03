using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using JamCat.Cameras;

public class ObjectJump : MonoBehaviour
{
    // Variables
    public Vector3 originalPos;
    public float jumpHeight = 0.1f;
    public bool isJumping;

    // Methods
    void Update() {
    }

    void OnAwake(){
        originalPos = transform.position;
    }

    public void Jump() {
        StartCoroutine(JumpCo(jumpHeight));
    }

    private IEnumerator JumpCo(float jumpHeightScale) {
            isJumping = true;
            originalPos = transform.position;

            float jumpStartTime = Time.time;
            float jumpDuration = 0.08f;
            /**
            jumpHeightScale = jumpHeightScale * carRigidbody2D.velocity.magnitude * 0.05f;
            jumpHeightScale = Mathf.Clamp(jumpHeightScale, 0.0f, 1.0f);
            carCollider2D.enabled = false;
            **/
            while (isJumping)
            {

                float jumpCompletedPercentage = (Time.time - jumpStartTime) / jumpDuration;
                jumpCompletedPercentage = Mathf.Clamp01(jumpCompletedPercentage);

                //transform.localScale = Vector3.one + Vector3.one  * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;
                //transform.localScale = carSpriteRenderer.transform.localScale * 0.75f;
                transform.position = originalPos + new Vector3(0.0f, 1.0f, 0.0f) * jumpHeightScale;
                //transform.position = originalPos;

                if (jumpCompletedPercentage == 1.0f)
                    break; 
                yield return null;
            }
            /**
            player.jumpFlag = false;
            carSpriteRenderer.transform.localScale = Vector3.one;
            carShadowRenderer.transform.localPosition = Vector3.zero;
            carShadowRenderer.transform.localScale = initialScale;
            carCollider2D.enabled = true;
            */
            transform.position = originalPos;
            isJumping = false;
            //accelerationFactor = 50;
            StopAllCoroutines();
        }
}
