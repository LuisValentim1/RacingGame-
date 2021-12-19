using System;
using System.Collections;
using UnityEngine;


namespace CatJam.Audios
{
    abstract public class Audio : MonoBehaviour {
        
        // Variables
        protected AudioSource audioSource;
        protected AudioClip clip;
        protected bool started;

        public float volume;

        // Methods -> Abstract
        abstract protected void OnAwake();
        abstract protected void OnStart();
        abstract protected void OnUpdate();

        // Methods -> Standard
        public void AwakeAudio() {

        }

        public void StartAudio() {

        }
        
        public void UpdateAudio() {

        }

        // Methods -> Public
        public void PlayAudio() {
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

        public void SetVolume(float value) {
            volume = Math.Clamp(value, 0, 1);
        }
    }
}