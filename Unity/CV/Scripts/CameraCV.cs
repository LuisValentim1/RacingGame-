using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCV : MonoBehaviour
{
    Vector3 initialPos;
    bool moving;
    float sens = 1.0f;
    
    private void Awake() {
        initialPos = transform.position;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            moving = true;
        }
        
        if (Input.GetMouseButtonUp(1)) {
            moving = false;
        }
    }

    void FixedUpdate() {
        if (moving == true)
            Interacting();
        else
            NotInteracting();
    }

    void Interacting() {
        transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens, 0.0f);
    }

    void NotInteracting() {
        transform.position = Vector3.Lerp(transform.position, initialPos, Time.deltaTime);
    }
}
