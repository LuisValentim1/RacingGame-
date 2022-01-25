using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnLights : MonoBehaviour
{
    bool stateM;
    bool stateA;
    public AudioSource sourceLightOff, sourceLightOn;
    public GameObject[] objLights;
    public Text txt;
    public Text txtA;

    private void Awake() {
        SwitchManual(false);
        SwitchAutomaticLights(true);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            SwitchAutomaticLights();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            SwitchManual();
        }
    }

    public void SwitchManual() {
        SwitchManual(!stateM);
    }

    public void SwitchManual(bool state) {
        if (stateA == true)
            return;
            
        this.stateM = state;
        for (int i = 0; i < objLights.Length; i++)
            objLights[i].SetActive(state);

        if (state == true) {
            sourceLightOn.Play();
            txt.text = "L - Switch Off Lights";
        } else {
            sourceLightOff.Play();
            txt.text = "L - Switch On Lights";
        }

    }
    


    public void SwitchAuto() {
        SwitchAuto(!stateA);
    }

    public void SwitchAuto(bool state) {
        if (stateA == false)
            return;

        if (stateM == state)
            return;
            
        this.stateM = state;
        for (int i = 0; i < objLights.Length; i++)
            objLights[i].SetActive(state);

        if (state == true) {
            sourceLightOn.Play();
            txt.text = "L - Switch Off Lights";
        } else {
            sourceLightOff.Play();
            txt.text = "L - Switch On Lights";
        }
    }



    public void SwitchAutomaticLights() {
        SwitchAutomaticLights(!stateA);
    }

    public void SwitchAutomaticLights(bool state) {
        stateA = state;

        if (stateA == true) {
            txtA.text = "A - Switch Off Automatic Lights";
        } else {
            txtA.text = "A - Switch On Automatic Lights";
        }
    }
}
