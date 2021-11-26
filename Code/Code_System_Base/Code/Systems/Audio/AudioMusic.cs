using System;
using System.Collections;
using UnityEngine;

namespace CatJam.Audio 
{
    public class AudioMusic : MonoBehaviour {

        // Variables
        [SerializeField] private AudioSource audioSource;
        private AudioClip clip;
        private bool started;

        public float volume;


        // Methods -> Standard
        public void AwakeAudio() {
            if(audioSource == null)
                audioSource.GetComponent<AudioSource>();

            started = false;
        }

        public void StartAudio() {

        }
        
        public void UpdateAudio() {

        }

        // Methods -> Public
        public void PlayMusic() {
            if(audioSource.clip == null)
                return;

            started = true;
            audioSource.Play();
        }

        public void PlayMusic(AudioClip clip) {
            SetAudioClip(clip);
            PlayMusic();
        }

        public void PauseMusic() {
            audioSource.Pause();
        }

        public void StopMusic() {
            audioSource.Stop();
        }


        // Methods -> Set
        public void SetAudioClip(AudioClip clip) {
            this.clip = clip;
        }

        public void SetVolume(float value) {
            volume = Math.Clamp(value, 0, 1);
        }
    }
}