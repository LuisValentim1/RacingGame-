using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLights : MonoBehaviour
{
    bool state;
    public AudioSource sourceLightOff, sourceLightOn;
    public GameObject[] objLights;

    private void Awake() {
        Switch(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            Switch();
        }
    }

    public void Switch() {
        Switch(!state);
    }

    public void Switch(bool state) {
        this.state = state;
        for (int i = 0; i < objLights.Length; i++)
            objLights[i].SetActive(state);

        if (state == true)
            sourceLightOn.Play();
        else
            sourceLightOff.Play();
    }
}
