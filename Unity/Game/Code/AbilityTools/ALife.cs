using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

public class ALife : MonoBehaviour
{
    public float countdownToDestroy;
    public int removeLifes;

    private void Update() {
        countdownToDestroy -= Time.deltaTime;
        if (countdownToDestroy <= 0)
            Destroy(gameObject);
    }
}
