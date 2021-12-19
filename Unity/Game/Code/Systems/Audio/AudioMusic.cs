using System;
using System.Collections;
using UnityEngine;

namespace CatJam.Audios
{
    public class AudioMusic : Audio {

        // Variables
        [SerializeField] private AudioSource audioSource;
        private AudioClip clip;
        private bool started;

        public float volume;


        // Methods -> Standard
        override protected void OnAwake() {
            if(audioSource == null)
                audioSource.GetComponent<AudioSource>();

            started = false;
        }

        override protected void OnStart() {

        }

        override protected void OnUpdate() {

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