using System;
using System.Collections;
using UnityEngine;

namespace CatJam.Cameras
{
    public class SysCamera : Sys 
    {
        
        // Variables
        public CameraBase current_camera;

        // Methods -> Override
        protected override void OnAwake() {

        }

        protected override void OnStart() {

        }

        protected override void OnUpdate() {
            current_camera.UpdateCamera();
        }
    }
}
