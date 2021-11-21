using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class UI : MonoBehaviour
{
    // Variables

    // Methods -> Abstract
    abstract protected void OnAwake();
    abstract protected void OnOpen();
    abstract protected void OnClose();
    abstract protected void OnUpdate();

    // Methods -> Public
    public void AwakeUI() {
        OnAwake();
    }

    public void OpenUI() {
        OnOpen();
    }

    public void CloseUI() {
        OnClose();
    }

    public void UpdateUI() {
        OnUpdate();
    }
}
