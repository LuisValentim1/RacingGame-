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
        public Audio[] audios;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            for (int i = 0; i < audios.Length; i++) {
                audios[i].AwakeAudio();
            }
        }

        protected override void OnStart() {
            for (int i = 0; i < audios.Length; i++) {
                audios[i].StartAudio();
            }
        }

        protected override void OnUpdate() {
            for (int i = 0; i < audios.Length; i++) {
                audios[i].UpdateAudio();
            }
        }

        // Methods -> Public
    }
}