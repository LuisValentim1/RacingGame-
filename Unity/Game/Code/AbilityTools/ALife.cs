using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

public class ALife : MonoBehaviour
{
    public float countdownToDestroy;
    public bool useAnimatorToDestroy;
    public int removeLifes;

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
