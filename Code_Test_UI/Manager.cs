using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Methods
    public Sys[] systems;

    // Methods -> Standard
    private void Awake() {
        for (int i = 0; i < systems.Length; i++)
            systems[i].AwakeSys();
    }

    private void Start() {
        for (int i = 0; i < systems.Length; i++)
            systems[i].StartSys();
    }

    private void Update() {
        for (int i = 0; i < systems.Length; i++)
            systems[i].UpdateSys();
    }
}
