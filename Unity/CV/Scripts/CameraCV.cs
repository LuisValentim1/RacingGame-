using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraCV : MonoBehaviour
{

    public Transform left, right, up, down;

    public GameObject shadersShow;
    public GameObject mainMenuMap;

    public PostProcessProfile postProcessProfile;

    Vector3 initialPos;
    bool moving;
    float sens = 1.0f;
    
    private void Awake() {
        initialPos = transform.position;
    }

    private void Update() {
        if (Data.Get().gameLogic.inMainMenu == true) {
            if (Input.GetMouseButtonDown(1)) {
                moving = true;
            }
            
            if (Input.GetMouseButtonUp(1)) {
                moving = false;
            }

            if (Input.GetKeyDown(KeyCode.Y)) {
                shadersShow.SetActive(!shadersShow.activeSelf);

                mainMenuMap.SetActive(!shadersShow.activeSelf);
                DepthOfField depthOfField = postProcessProfile.GetSetting<DepthOfField>();
                depthOfField.active = !shadersShow.activeSelf;
            }
        }
    }

    void FixedUpdate() {
        if (moving == true)
            Interacting();
        else
            NotInteracting();
    }

    void Interacting() {
        Vector3 newPos = transform.position + new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens, 0.0f);
        
        if (newPos.x < left.position.x) {
            newPos.x = left.position.x;
        } else if (newPos.x > right.position.x) {
            newPos.x = right.position.x;
        }

        if (newPos.y < down.position.y) {
            newPos.y = down.position.y;
        } else if (newPos.y > up.position.y) {
            newPos.y = up.position.y;
        }
        
        transform.position = newPos;
    }

    void NotInteracting() {
        transform.position = Vector3.Lerp(transform.position, initialPos, Time.deltaTime);
    }
}
