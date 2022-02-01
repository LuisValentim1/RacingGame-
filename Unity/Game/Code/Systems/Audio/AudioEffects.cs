using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

namespace JamCat.Audio {
    public class AudioEffects : MonoBehaviour {
    
        public static AudioEffects instance;
        public static AudioEffects Get() { return instance; }

        // Variables
        private List<AudioEffect> allAudioEffects;   
        public GameObject prefabAudioEffect;

        float volume;

        // Methods
        public void AwakeAudio() {
            allAudioEffects = new List<AudioEffect>();
            instance = this;
        }

        public void StartAudio() {

        }

        public void UpdateAudio() {
            for (int i = 0; i < allAudioEffects.Count; i++) {
                allAudioEffects[i].UpdateEffect();
            }
        }

        public void PlayAudioEffect(Transform point, AudioClip clip) {
            GameObject newObj = null;
            if (point != null)
                newObj = Instantiate(prefabAudioEffect, point.position, Quaternion.identity, transform);
            else 
                newObj = Instantiate(prefabAudioEffect, Vector3.zero, Quaternion.identity, transform);

            AudioEffect audioEffect = newObj.GetComponent<AudioEffect>();
            audioEffect.getAudioSource().volume = Data.Get().options.audioGeneralVolume * Data.Get().options.audioEffectsVolume;
            audioEffect.Play(clip);
        }

        public void setVolume(float value) {
            volume = value;
            for (int i = 0; i < allAudioEffects.Count; i++)
                allAudioEffects[i].getAudioSource().volume = volume;
        }
    }
}
