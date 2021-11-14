using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Sys : MonoBehaviour
{
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
        OnUpdate();
    }
}
