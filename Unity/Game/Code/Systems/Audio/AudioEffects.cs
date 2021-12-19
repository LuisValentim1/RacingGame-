using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatJam.Audios 
{
    public class AudioEffects : MonoBehaviour 
    {
    
        // Variables
        private List<AudioEffect> all_audio_effects;   
        public GameObject prefab_audio_effect;

        // Methods
        public void UpdateAudio() {
            for (int i = 0; i < all_audio_effects.Count; i++) {
                all_audio_effects[i].UpdateEffect();
            }
        }

        public void PlayAudioEffect(Transform point, AudioClip clip) {
            GameObject newObj = Instantiate(prefab_audio_effect, point.position, Quaternion.identity, transform);
            AudioEffect audioEffect = newObj.GetComponent<AudioEffect>();
            audioEffect.Play(clip);
        }
    }
}
