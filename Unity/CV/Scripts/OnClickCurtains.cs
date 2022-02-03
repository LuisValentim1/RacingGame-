using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using JamCat.Cameras;

public class OnClickCurtains : MonoBehaviour
{
    // Variables
    public LayerMask layerMask;
    public bool onClicked;
    public AudioSource audioSource;

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
                    if (hit.collider.tag == "cv_Curtains") {
                        if (GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Wind") == false) {
                            audioSource.Play();
                            GetComponentInParent<Animator>().SetTrigger("Wind");
                        }             
                    }
                }
            }
        }
    }
}
