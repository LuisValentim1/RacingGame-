using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Cameras;

public class CatArm : MonoBehaviour
{
    public LayerMask layerMask;


    void Update()
    {
        
    }

    void FixedUpdate(){
        UpdateRaycast();
    }

    void UpdateRaycast() {
        RaycastHit hit;
        Ray ray = SysCamera.Get().getCurrentCamera().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
            if (hit.collider != null) {
                transform.parent.transform.LookAt(hit.point);

                if (hit.collider.tag == "cv_Cat") {
                    //GameObject arm = FindComponentInChildWithTag(hit.collider.gameObject, "cv_Arm");
                    //if( arm != null){
                      //  arm.GetComponent<ArmMove>().Move(hit.point);    
                }             
            }
        }
    }

    GameObject FindComponentInChildWithTag(GameObject parent, string tag){
        Transform t = parent.transform;
        foreach(Transform tr in t)
        {
                if(tr.tag == tag)
                {
                    return tr.gameObject;
                }
        }
        return null;
    }
}