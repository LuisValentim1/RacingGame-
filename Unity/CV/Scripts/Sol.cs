using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sol : MonoBehaviour
{

    public float offsetZ;

    public bool fastForwardActivated = false;
    public float rotSpeed = 1;
    public float rotZ, rotX;

    private void Awake() {

    }

    private void Start() {
        
    }

    private void Update() {
        if (Data.Get().gameLogic.in_main_menu == true) {
            if (Input.GetKeyDown(KeyCode.Keypad8))
                ResetClass();

            if (Input.GetKeyDown(KeyCode.Keypad9))
                ToggleFastForward();


            if (fastForwardActivated == true) {
                Vector3 euler = transform.localEulerAngles;
                euler.x += Time.deltaTime * rotSpeed;
                rotX = euler.x;
                transform.localEulerAngles = euler;
            } else {
                Vector3 euler = transform.localEulerAngles;
                System.DateTime curDateTime = System.DateTime.Now;
                // float value = 360 / (curDateTime.Hour * 60 + curDateTime );
            }
        }
    }

    public void ResetClass() {

    }

    public void ToggleFastForward() {
        fastForwardActivated = !fastForwardActivated;

        if (fastForwardActivated == true) {

        } else {

        }
    }
}
