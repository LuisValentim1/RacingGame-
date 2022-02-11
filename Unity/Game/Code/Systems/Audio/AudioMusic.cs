using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Audio 
{
    public class AudioMusic : Audio {

        public static AudioMusic instance;
        public static AudioMusic Get() { return instance; }

        // Variables
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips;

        protected AudioClip clip;

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
        public void PlayMusic() {
            if(audioSource.clip == null)
                return;

            // print(0);
            started = true;
            audioSource.Play();
        }

        public void PlayMusic(int number) {
            setAudioClip(audioClips[number]);
            PlayMusic();
        }

        public void PlayMusic(AudioClip clip) {
            setAudioClip(clip);
            PlayMusic();
        }

        public void PauseMusic() {
            audioSource.Pause();
        }

        public void StopMusic() {
            audioSource.Stop();
        }


        // Methods -> Set
        public void setAudioClip(AudioClip clip) {
            this.clip = clip;
            audioSource.clip = clip;
        }

        public void setVolume(float value) {
            volume = Math.Clamp(value, 0, 1);
            audioSource.volume = volume;
        }
    }
}