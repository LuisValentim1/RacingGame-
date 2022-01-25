using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Cameras;

public class OnClickCat : MonoBehaviour
{
    // Variables
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private int num;

    // Methods
    void Update() {
        UpdateRaycast();
    }

    void UpdateRaycast() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = SysCamera.Get().getCurrentCamera().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider != null) {
                    if (hit.collider.gameObject == gameObject) {
                        if (GetComponentInParent<Animator>() != null) {
                            if (GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pulo") == false) {
                                GetComponent<Animator>().SetTrigger("Trigger");
                                PlayAudio();
                            }
                        } else {
                            PlayAudio();
                        }
                       
                    }
                }
            }
        }
    }
    
    void PlayAudio() {
        audioSource.clip = audioClips[num];
        audioSource.Play();
        num++;
        if (num >= audioClips.Length)
            num = 0;
    }
}
