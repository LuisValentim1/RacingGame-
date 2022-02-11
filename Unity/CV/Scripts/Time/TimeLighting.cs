using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLighting : MonoBehaviour
{
    public Light lightSun;
    public float lightSun_offsetX;
    public float lightSun_offsetY;
    public Vector3 lightSun_StartEuler;
    public Vector3 lightSun_CurrentEuler;

    public void Start() {
        lightSun_CurrentEuler = lightSun_StartEuler;
        lightSun.transform.localEulerAngles = lightSun_CurrentEuler;
    }

    public void Update() {
        if (Data.Get().gameLogic.inMainMenu == true)
            UpdateSun();
    }

    void UpdateSun() {
        SimpleSun_Rotation();
    }

    void SimpleSun_Rotation() {
        float timeNormalized = TimeCalendar.Get().GetTimeNormalized();
        lightSun_CurrentEuler.x = 360 * timeNormalized + lightSun_offsetX;
        lightSun.transform.localEulerAngles = lightSun_CurrentEuler;        
    }

    public void OnUpdateDayState(DayState dayState) {
        switch (dayState) {
            case DayState.day:
                GetComponent<TurnLights>().SwitchAuto(false);
                lightSun.shadows = LightShadows.Hard;
                break;
            case DayState.sunset:
                GetComponent<TurnLights>().SwitchAuto(false);
                lightSun.shadows = LightShadows.Hard;
                break;
            case DayState.night:
                GetComponent<TurnLights>().SwitchAuto(true);
                lightSun.shadows = LightShadows.None;
                break;
            case DayState.sunrise:
                GetComponent<TurnLights>().SwitchAuto(false);
                lightSun.shadows = LightShadows.Hard;
                break;
        }
    }



    public float GetDistMap(float value, float valueRange, float from1, float to1, float from2, float to2)
    {
        float mapValue = (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        float mapValueRange = (valueRange - from1) / (to1 - from1) * (to2 - from2) + from2;
        return Mathf.Abs(mapValueRange - mapValue);
    }
}