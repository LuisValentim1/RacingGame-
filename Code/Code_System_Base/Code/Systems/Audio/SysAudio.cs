using System;
using System.Collections;
using UnityEngine;

namespace CatJam.Audio 
{
    public class SysAudio : Sys {

        // Instance
        public static SysAudio instance;
        public static SysAudio Get() { return instance; }    

        // Variables
        public Audio[] audios_type;
        public GameObject prefab_audio_effect;

        // Methods -> Override
        protected override void OnAwake() {
            for (int i = 0; i < audios_type.Length; i++)
                audios_type[i].AwakeAudio();
        }

        protected override void OnStart() {
            for (int i = 0; i < audios_type.Length; i++)
                audios_type[i].StartAudio();
        }

        protected override void OnUpdate() {
            for (int i = 0; i < audios_type.Length; i++)
                audios_type[i].UpdateAudio();
        }


        // Methods -> Public
        public void PlayAudioEffect(AudioClip clip) {

        }

        public void PlayMusic() {

        }
    }
}