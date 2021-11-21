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

        Window_Options window_options = Window_Options.Get();
        window_options.LoadAndApply();
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