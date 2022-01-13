using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

public class ASlow : MonoBehaviour
{
    public float countdownToDestroy;
    public float slowDuration;
    public float slowIntensity;
    public float maxAcceleration;

    private void Update() {
        countdownToDestroy -= Time.deltaTime;
        if (countdownToDestroy <= 0)
            Destroy(gameObject);
    }
}
