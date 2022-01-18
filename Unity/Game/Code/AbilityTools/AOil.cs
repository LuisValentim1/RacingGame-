using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

public class AOil : MonoBehaviour
{
    [Header("Oil Configuration")]
    public float timerEffect;
    public float countdownToDestroy;

    private void Update() {
        countdownToDestroy -= Time.deltaTime;
        if (countdownToDestroy <= 0)
            Destroy(gameObject);
    }
}
