using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class UI : MonoBehaviour
{
    // Variables -> Instance
    public static UI instance;
    public static UI Get() { return instance; }

    // Variables
    public float alpha_value;
    public bool hide;

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
