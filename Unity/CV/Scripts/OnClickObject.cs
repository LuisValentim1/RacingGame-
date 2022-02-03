using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using JamCat.Cameras;

public class OnClickObject : MonoBehaviour
{
    // Variables
    public LayerMask layerMask;
    public bool onClicked;
    public AudioClip[] audioClips;

    // Methods
    void Update() {
        UpdateRaycast();
    }

    void UpdateRaycast() {
        if (EventSystem.current.IsPointerOverGameObject() == true)
            return;

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = SysCamera.Get().getCurrentCamera().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                if (hit.collider != null) {
                    if (hit.collider.tag == "UI")
                        return;

                    if (hit.collider.tag == "cv_Movable") {
                        if(hit.collider.gameObject.GetComponent<ObjectJump>() == null){
                            hit.collider.gameObject.AddComponent<ObjectJump>();
                        }
                        hit.collider.gameObject.GetComponent<ObjectJump>().Jump();

                        if(hit.collider.gameObject.GetComponent<AudioSource>() == null){
                            hit.collider.gameObject.AddComponent<AudioSource>();
                        }
                        hit.collider.gameObject.GetComponent<AudioSource>().clip = audioClips[Random.Range(0, audioClips.Length)];
                        hit.collider.gameObject.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }
    }
}
