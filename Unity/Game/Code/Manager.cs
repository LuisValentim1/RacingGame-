using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JamCat.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public static Manager Get() { return instance; }

    // Methods
    public Sys[] systems;
    public Data data;

    public bool dev_tools = true;
    public bool hacks = false;

    // Methods -> Standard
    private void Awake() {
        instance = this;
        data.OnAwake();
        for (int i = 0; i < systems.Length; i++)
            systems[i].AwakeSys();
    }

    private void Start() {
        for (int i = 0; i < systems.Length; i++)
            systems[i].StartSys();

        Window_Options.Get().LoadAndApply();
        GeneralMethods.MainMenu();
    }

    private void Update() {
        for (int i = 0; i < systems.Length; i++)
            systems[i].UpdateSys();


        if (dev_tools == true) {
            Dev_InputToSkip();
        }
    }


    public void Dev_InputToSkip() {
        if (Input.GetKeyDown(KeyCode.O))
            SkipToGame(0);
            /*
        } else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            SkipToGame(1);
        } else if (Input.GetKeyDown(KeyCode.Keypad3)) {
            SkipToGame(2);
        } else if (Input.GetKeyDown(KeyCode.Keypad4)) {
            SkipToGame(3);
        }
        */
    }

    public void SkipToGame(int characterNumber) {

        if (Data.Get().gameLogic.in_main_menu == true) {
            Window_MainMenu.Get().Button_Play();
            Window_Lobby.Get().ButtonHost();
            Window_CharacterSelection.Get().ButtonSelectCharacter(characterNumber);
            Window_CharacterSelection.Get().ToggleReady();
        }
        
        if (Data.Get().gameLogic.game_finished == true) {
            Window_Finish.Get().Button_PlayAgain();
        }
        
        if (Data.Get().gameLogic.in_game == true) {
            GeneralMethods.StartGame();
        }
        
        
       // GeneralMethods.StartGame();
    }
}