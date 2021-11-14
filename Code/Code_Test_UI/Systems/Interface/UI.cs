using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class UI : MonoBehaviour
{
    // Methods -> Abstract
    abstract protected void OnAwake();
    abstract protected void OnStart();
    abstract protected void OnUpdate();

    // Methods -> Public
    public void AwakeUI() {
        OnAwake();
    }

    public void StartUI() {
        OnStart();
    }

    public void UpdateUI() {
        OnUpdate();
    }
}
