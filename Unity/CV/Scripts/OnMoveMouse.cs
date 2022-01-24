using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamCat.Cameras;

public class OnMoveMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        UpdateRaycast();
    }

    void UpdateRaycast() {
        RaycastHit hit;
        Ray ray = SysCamera.Get().getCurrentCamera().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider != null) {
                if (hit.collider.tag == "cv_Cat") {
                    GameObject arm = FindComponentInChildWithTag(hit.collider.gameObject, "cv_Arm");
                    if( arm != null){
                        arm.GetComponent<ArmMove>().Move(hit.point);    
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
}
