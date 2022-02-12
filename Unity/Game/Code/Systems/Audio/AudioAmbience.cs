using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Audio 
{
    public class AudioAmbience : Audio {

        public static AudioAmbience instance;
        public static AudioAmbience Get() { return instance; }

        // Variables

        // Methods -> Standard
        protected override void OnAwake() {
            instance = this;
        }

        protected override void OnStart() {
        
        }

        protected override void OnUpdate() {

        }
        
        protected override void OnPlay() {
            audioSource.Play();
        }

        // Methods -> Public

        // Methods -> Set
    }
}