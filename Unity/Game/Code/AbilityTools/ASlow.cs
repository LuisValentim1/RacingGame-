using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

public class ASlow : MonoBehaviour
{
    public float countdownToDestroy;
    public bool useAnimatorToDestroy;

    public float slowDuration;
    public float slowIntensity;
    public float maxAcceleration;

    private void Update() {
        if (useAnimatorToDestroy == true) {
            countdownToDestroy -= Time.deltaTime;
            if (countdownToDestroy <= 0)
                GetComponentInChildren<Animator>().SetBool("Despawn", true);
        }else {
            countdownToDestroy -= Time.deltaTime;
            if (countdownToDestroy <= 0)
                Destroy(gameObject);
        }
    }
}
