using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;

namespace JamCat.Audio {
    public class AudioEffects : Audio {
    
        public static AudioEffects instance;
        public static AudioEffects Get() { return instance; }

        // Variables
        private List<AudioEffect> allAudioEffects;   
        public GameObject prefabAudioEffect;

        // Methods
        protected override void OnAwake() {
            allAudioEffects = new List<AudioEffect>();
            instance = this;
        }

        protected override void OnStart() {
        
        }

        protected override void OnUpdate() {
            for (int i = 0; i < allAudioEffects.Count; i++) {
                allAudioEffects[i].UpdateEffect();
            }
        }


        public void PlayAudioEffect(Transform point, AudioClip clip) {
            if (clip == null)
                return;

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