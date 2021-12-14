using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CatJam.UI;

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

        Dev_InputToSkip();
    }


    public void Dev_InputToSkip() {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            SkipToGame(0);
        } else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            SkipToGame(1);
        } else if (Input.GetKeyDown(KeyCode.Keypad3)) {
            SkipToGame(2);
        } else if (Input.GetKeyDown(KeyCode.Keypad4)) {
            SkipToGame(3);
        }
    }

    public void SkipToGame(int characterNumber) {
        Window_MainMenu.Get().Button_Play();
        Window_CharacterSelection.Get().Button_Select_Character(characterNumber);
        Window_CharacterSelection.Get().Button_Play();
        GeneralMethods.StartGame();
    }
}