using System;
using System.Collections;
using UnityEngine;


namespace CatJam.Audio 
{
    public class AudioEffect : Audio {
        
        // Variables
        

        // Methods -> Override
        override protected void OnAwake() {

        }

        protected override void OnStart() {

        }

        protected override void OnUpdate() {
            if (started == true && audioSource.isPlaying == false)
                Destroy(gameObject);
        }

        
        override protected void OnPlay() {

        }

        override protected void OnStop() {

        }
    }
}