using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

public class AOil : MonoBehaviour
{
    private bool selfDestroyOnHit;
    private string tagHit;

    [Header("Oil Configuration")]
    public float timerEffect;
    public float countdownToDestroy;

    private void Update() {
        countdownToDestroy -= Time.deltaTime;
        if (countdownToDestroy <= 0)
            Destroy(gameObject);
    }
    
    public void HitTarget(string tag) {
        Destroy(gameObject);
        if (selfDestroyOnHit == true) {
            if (tagHit == tag) {
                Destroy(gameObject);
            }
        }
    }

    public void setSelfDestroyOnHit(bool selfDestroyOnHit, string tagHit) {
        this.selfDestroyOnHit = selfDestroyOnHit;
        this.tagHit = tagHit;
    }
}
