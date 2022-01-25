using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Audio 
{
    public class AudioAmbience : MonoBehaviour {

        public static AudioAmbience instance;
        public static AudioAmbience Get() { return instance; }

        // Variables
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips;

        private AudioClip clip;
        private bool started;
        private float volume;

        // Methods -> Standard
        public void AwakeAudio() {
            if(audioSource == null)
                audioSource.GetComponent<AudioSource>();

            started = false;
            instance = this;
        }

        public void StartAudio() {

        }
        
        public void UpdateAudio() {

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