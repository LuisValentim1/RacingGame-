using System;
using System.Collections;
using UnityEngine;


namespace JamCat.Audio 
{
    abstract public class Audio : MonoBehaviour {
        
        // Variables
        protected bool started;

        public float volume;

        // Methods -> Abstract
        abstract protected void OnAwake();
        abstract protected void OnStart();
        abstract protected void OnUpdate();

        // Methods -> Standard
        public void AwakeAudio() {
            OnAwake();
        }

        public void StartAudio() {
            OnStart();
        }
        
        public void UpdateAudio() {
            OnUpdate();
        }
    }
}