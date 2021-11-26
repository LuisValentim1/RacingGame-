using System;
using System.Collections;
using UnityEngine;


namespace CatJam.Audio 
{
    public class AudioEffect : MonoBehaviour {
        
        // Variables
        private AudioSource audioSource;
        private bool started; 

        // Methods -> Override
        public void UpdateEffect() {
            if (started == true && audioSource.isPlaying == false)
                Destroy(gameObject);
        }

        public void Play(AudioClip clip) {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
        }

    }
}