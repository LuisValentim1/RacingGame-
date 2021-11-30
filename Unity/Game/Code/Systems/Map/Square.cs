using System;
using System.Collections;
using UnityEngine;

public class Square : MonoBehaviour {
    
    // Variables
    public Element element;

    // Methods
    public void OnStart() {
        element = null;
    }


    // Methods -> Get
    public bool GetIsEmpty() {
        if (element == null) 
            return true;
        return false;
    }
}