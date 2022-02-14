using System;
using System.Collections;
using UnityEngine;

public class ElementTree : Element {

    // Variables
    [Header("Configurable")]
    [SerializeField] private GameObject objTrunkMask;
    [Header("On Collide")]
    [SerializeField] [Range(1, 10)] private float velocityDivide = 3;
    [SerializeField] [Range(1, 50)] private float accelerationFactor = 30;

    // Methods - Get   
    public float GetVelocityDivide() { return velocityDivide; }
    public float GetAccelerationFactor() { return accelerationFactor; }


    public void ActivateTrunkMask() {
        objTrunkMask.SetActive(true);
    }
}