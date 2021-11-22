using System;
using System.Collections;
using UnityEngine;


namespace CatJam.Audio 
{
    abstract public class Audio : MonoBehaviour {
        
        // Variables
        protected AudioSource audioSource;
        protected AudioClip clip;
        protected bool started;

        // Methods -> Abstract
        abstract protected void OnAwake();
        abstract protected void OnStart();
        abstract protected void OnUpdate();
        abstract protected void OnPlay();
        abstract protected void OnStop();

        // Methods -> Standard
        public void AwakeAudio() {

        }

        public void StartAudio() {

        }
        
        public void UpdateAudio() {

        }

        // Methods -> Public
        public void PlayAudio() {
            OnPlay();
            started = true;
        }

        public void PlayAudio (AudioClip clip) {
            SetAudio(clip);
            PlayAudio();
        }

        public void StopAudio() {

        }

        public void SetAudio(AudioClip clip) {
            this.clip = clip;
        }
    }
}