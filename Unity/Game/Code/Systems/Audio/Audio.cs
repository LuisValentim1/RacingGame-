using System;
using System.Collections;
using UnityEngine;


namespace JamCat.Audio 
{
    abstract public class Audio : MonoBehaviour {
        
        // Variables
        protected bool started;
        protected float volume;
        
        [SerializeField] protected AudioSource audioSource;
        [SerializeField] protected AudioClip[] audioClips;

        protected AudioClip clip;

        // Methods -> Abstract
        abstract protected void OnAwake();
        abstract protected void OnStart();
        abstract protected void OnUpdate();
        abstract protected void OnPlay();

        // Methods -> Standard
        public void AwakeAudio() {
            if(audioSource != null)
                audioSource.GetComponent<AudioSource>();

            started = false;
            OnAwake();
        }

        public void StartAudio() {
            OnStart();
        }
        
        public void UpdateAudio() {
            OnUpdate();
        }

        public void Play() {
            OnPlay();
            started = true;
        }
        
        public void Play(int number) {
            setAudioClip(audioClips[number]);
            Play();
        }

        public void Play(AudioClip clip) {
            setAudioClip(clip);
            Play();
        }

        
        public void PauseMusic() {
            if(audioSource != null)
                audioSource.Pause();
        }

        public void StopMusic() {
            if(audioSource != null)
                audioSource.Stop();
        }

        public void setAudioClip(AudioClip clip) {
            this.clip = clip;
            if(audioSource != null)
                audioSource.clip = clip;
        }

        public virtual void setVolume(float value) {
            volume = Math.Clamp(value, 0, 1);
            if(audioSource != null)
                audioSource.volume = volume;
        }
    }
}