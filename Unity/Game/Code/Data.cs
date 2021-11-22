using System;
using System.Collections;
using UnityEngine;

public class Data : MonoBehaviour {

    // Variables -> Instance
    public static Data instance;
    public static Data Get() { return instance;}

    public static Options options;

    // Variables -> Public
    public bool is_paused;
    public bool in_game;
    public bool in_main_menu;

    // Methods -> Standard
    public void OnAwake() {
        instance = this;
    }

    // Serializables

    [Serializable]
    public class Options {
        public int quality_level;

        public int textures_quality;
        public int lighting_quality;
        public bool enable_shadows;
        public int shadows_quality;
    
        public float audio_general_volume;
        public float audio_music_volume;
        public float audio_effects_volume;
        public float audio_dialogues_volume;

        public char controls_accelerate;
        public char controls_brake;
        public char controls_turn_right;
        public char controls_turn_left;
        public char controls_use_skill_1;
        public char controls_use_skill_2;
        public char controls_pause_menu;
    }
}