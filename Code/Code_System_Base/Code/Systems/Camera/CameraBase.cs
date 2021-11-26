using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CatJam.Cameras
{
    abstract public class CameraBase : MonoBehaviour
    {
        // Methods -> Abstract
        abstract protected void OnUpdate();
    
        // Methods -> Standard
        public void UpdateCamera() {
            OnUpdate();
        }
    }
}