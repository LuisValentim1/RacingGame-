using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Sys : MonoBehaviour
{
    // Variables -> Static
    public static Sys instance;
    public static Sys Get() { return instance; }

    // Variables
    public bool can_pause;

    // Methods -> Abstract
    abstract protected void OnAwake();
    abstract protected void OnStart();
    abstract protected void OnUpdate();

    // Methods -> Public
    public void AwakeSys() {
        OnAwake();
    }

    public void StartSys() {
        OnStart();
    }

    public void UpdateSys() {
        if (can_pause == false && Data.Get().is_paused == false)
            OnUpdate();
    }
}