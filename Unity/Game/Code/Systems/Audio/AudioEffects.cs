using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamCat.Audio {
    public class AudioEffects : MonoBehaviour {
    
        // Variables
        private List<AudioEffect> allAudioEffects;   
        public GameObject prefabAudioEffect;

        // Methods
        public void AwakeAudio() {
            allAudioEffects = new List<AudioEffect>();
        }

        public void StartAudio() {

        }

        public void UpdateAudio() {
            for (int i = 0; i < allAudioEffects.Count; i++) {
                allAudioEffects[i].UpdateEffect();
            }
        }

        public void PlayAudioEffect(Transform point, AudioClip clip) {
            GameObject newObj = Instantiate(prefabAudioEffect, point.position, Quaternion.identity, transform);
            AudioEffect audioEffect = newObj.GetComponent<AudioEffect>();
            audioEffect.Play(clip);
        }
    }
}
