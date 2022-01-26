using System;
using System.Collections;
using UnityEngine;

public class Data : MonoBehaviour {

    // Variables -> Instance
    public static Data instance;
    public static Data Get() { return instance;}

    public GameLogic gameLogic;
    public SceneConfiguration sceneConfiguration;
    public GameData gameData;
    public Options options;

    // Methods -> Standard
    public void OnAwake() {
        instance = this;
        gameLogic = new GameLogic();
        gameData = new GameData();
        options = new Options();


    }


    // Serializables

    [Serializable]
    public class GameLogic {
        public bool is_paused = false;
        public bool in_game = false;
        public bool in_main_menu = true;
        public bool game_finished = false;
    }

    [Serializable]
    public class SceneConfiguration {
        public GameObject scene_main_menu;
        public GameObject scene_play;
    }

    [Serializable]
    public class GameData {
        public int characterSelected = -1;

        public bool localMode = false;
        public int[] charactersSelected;
    }

    [Serializable]
    public class Options {
        public int quality_level = 0;
    
        public float audioGeneralVolume = 0.8f;
        public float audioMusicVolume = 0.5f;
        public float audioAmbienceVolume = 0.5f;
        public float audioEffectsVolume = 0.8f;

        public char controls_accelerate = 'w';
        public char controls_brake = 's';
        public char controls_turn_right = 'd';
        public char controls_turn_left = 'a';
        public char controls_use_skill_1 = 'k';
        public char controls_use_skill_2 = 'l';
        public char controls_pause_menu = 'p';
    }
}