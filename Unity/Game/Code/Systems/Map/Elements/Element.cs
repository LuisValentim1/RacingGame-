using System;
using System.Collections;
using UnityEngine;
using JamCat.Map;

abstract public class Element : MonoBehaviour {
    
    // Variables
    public bool dontSync;
    public int elementID;

    private Module fromModule;
    private int moduleID;

    public bool onSpawn_MakeRotationZero;

    private void Start() {
        if (onSpawn_MakeRotationZero == true)
            transform.rotation = Quaternion.identity;
    }

    public void setModule(Module module, int moduleID) {
        this.fromModule = module;
        this.moduleID = moduleID;
    }
}