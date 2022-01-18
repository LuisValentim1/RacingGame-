using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
// using JamCat.Cameras;
=======
using JamCat.Cameras;
>>>>>>> Stashed changes

public class OnClickObject : MonoBehaviour
{
    // Variables
    public bool onClicked;
<<<<<<< Updated upstream
=======
    public Camera camera;
>>>>>>> Stashed changes

    // Methods
    void Update() {
        UpdateRaycast();
    }

    void UpdateRaycast() {
        //RaycastHit2D hit = Physics2D.Raycast(SysCamera.Get().currentCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
       // if (hit.collider != null) {

       // }
<<<<<<< Updated upstream
    }
=======

       if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if(hit.collider.gameObject.GetComponent<ObjectJump>() == null){
                        hit.collider.gameObject.AddComponent<ObjectJump>();
                    }
                    hit.collider.gameObject.GetComponent<ObjectJump>().Jump();                 
                }
            }
        }
    }


>>>>>>> Stashed changes
}
