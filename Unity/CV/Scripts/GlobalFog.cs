using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalFog : MonoBehaviour
{
    bool fogActivated;
    public GameObject objSun;
    public Text txt;

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            ToggleFog();
        }
    }

    public void ToggleFog() {
        fogActivated = !fogActivated;
        if (fogActivated == true) {
            objSun.SetActive(false);
            RenderSettings.fogDensity = 0.5f;
            txt.text = "F - Turn Off Fog";
        } else {
            objSun.SetActive(true);
            RenderSettings.fogDensity = 0f;
            txt.text = "F - Turn On Fog";
        }
    }
}
