using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Audio 
{
    public class SysAudio : Sys {

        // Instance
        public static SysAudio instance;
        public static SysAudio Get() { return instance; }    

        // Variables
        public AudioAmbience audioAmbience;
        public AudioEffects audioEffects;
        public AudioMusic audioMusic;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            audioAmbience.AwakeAudio();
            audioEffects.AwakeAudio();
            audioMusic.AwakeAudio();
        }

        protected override void OnStart() {
            audioAmbience.StartAudio();
            audioEffects.StartAudio();
            audioMusic.StartAudio();
        }

        protected override void OnUpdate() {
            audioAmbience.UpdateAudio();
            audioEffects.UpdateAudio();
            audioMusic.UpdateAudio();
        }


        // Methods -> Public
    }
}