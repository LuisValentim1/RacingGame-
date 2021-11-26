using System;
using System.Collections;
using UnityEngine;

public class UI_ToggleGroup : MonoBehaviour {
    
    // Variables
    [SerializeField] private UI_Toggle[] toggles;

    // Methods
    public void ActivateToggle(int number) {
        for (int i = 0; i < toggles.Length; i++)
            if (i != number)
                toggles[i].Activate(false);
        
        toggles[number].Activate(true);
    }
}