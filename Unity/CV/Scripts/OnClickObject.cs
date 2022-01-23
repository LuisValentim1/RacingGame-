using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Cameras;

public class OnClickObject : MonoBehaviour
{
    // Variables
    public bool onClicked;

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
                    if (hit.collider.tag == "cv_Movable") {
                        if(hit.collider.gameObject.GetComponent<ObjectJump>() == null){
                            hit.collider.gameObject.AddComponent<ObjectJump>();
                        }
                        hit.collider.gameObject.GetComponent<ObjectJump>().Jump();    
                    }             
                }
            }
        }
    }
}
