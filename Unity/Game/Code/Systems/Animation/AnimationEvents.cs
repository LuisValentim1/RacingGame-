using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Audio;

public class AnimationEvents : MonoBehaviour
{
    public AudioClip[] audioClips;

    public void PlaySound(int i) {
        AudioEffects.Get().Play(audioClips[i]);
    }


    public void DestroyThis() {
        Destroy(transform.parent.gameObject);
    }
    
    public void DestroyParent() {
        Destroy(transform.parent.gameObject);
    }
}
