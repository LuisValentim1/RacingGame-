using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Audio 
{
    public class AudioAmbience : Audio {

        public static AudioAmbience instance;
        public static AudioAmbience Get() { return instance; }

        // Variables
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips;

        private AudioClip clip;

        // Methods -> Standard
        protected override void OnAwake() {
            if(audioSource == null)
                audioSource.GetComponent<AudioSource>();

            started = false;
            instance = this;
        }

        protected override void OnStart() {
        
        }

        protected override void OnUpdate() {

        }

        // Methods -> Public
        public void PlayAmbience() {
            if(audioSource.clip == null)
                return;

            started = true;
            audioSource.Play();
        }

        public void PlayAmbience(int number) {
            setAudioClip(audioClips[number]);
            PlayAmbience();
        }

        public void PlayAmbience(AudioClip clip) {
            setAudioClip(clip);
            PlayAmbience();
        }

        public void PauseAmbience() {
            audioSource.Pause();
        }

        public void StopAmbience() {
            audioSource.Stop();
        }


        // Methods -> Set
        public void setAudioClip(AudioClip clip) {
            this.clip = clip;
        }

        public void setVolume(float value) {
            volume = Math.Clamp(value, 0, 1);
            audioSource.volume = volume;
        }
    }
}