using System;
using System.Collections;
using UnityEngine;

public class ElementBarrel : Element {

    // Variables
    [Header("On Collide")]
    [SerializeField] [Range(1, 10)] private float velocityDivide = 3;
    [SerializeField] [Range(1, 50)] private float accelerationFactor = 30;

    public GameObject objSprite1, objAnimation;

    public void StartAnimation() {
        objSprite1.SetActive(false);
        objAnimation.SetActive(true);
        GetComponentInChildren<Animator>().SetBool("hit", true);
    }

    // Methods - Get   
    public float GetVelocityDivide() { return velocityDivide; }
    public float GetAccelerationFactor() { return accelerationFactor; }
}