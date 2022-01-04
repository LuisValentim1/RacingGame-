using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CatJam.Cameras;

public class OnClickObject : MonoBehaviour
{
    // Variables
    public bool onClicked;

    // Methods
    void Update() {
        UpdateRaycast();
    }

    void UpdateRaycast() {
        //RaycastHit2D hit = Physics2D.Raycast(SysCamera.Get().currentCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
       // if (hit.collider != null) {

       // }
    }
}
