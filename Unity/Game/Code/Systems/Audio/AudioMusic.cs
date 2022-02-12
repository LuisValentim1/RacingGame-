using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Audio 
{
    public class AudioMusic : Audio {

        public static AudioMusic instance;
        public static AudioMusic Get() { return instance; }

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