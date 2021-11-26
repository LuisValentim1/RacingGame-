using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Methods
    public Sys[] systems;
    public Data data;

    // Methods -> Standard
    private void Awake() {
        data.OnAwake();
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