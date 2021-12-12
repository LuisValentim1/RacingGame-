using System;
using System.Collections;
using UnityEngine;

namespace CatJam.Cameras
{
    public class SysCamera : Sys 
    {
        // Instance
        public static SysCamera instance;
        public static SysCamera Get() { return instance; }
        
        // Variables
        public CameraBase current_camera;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
        }

        protected override void OnStart() {

        }

        protected override void OnUpdate() {
            current_camera.UpdateCamera();
        }

        // Methods -> Public
        public void AutoConfigureCamera() {

        }
    }
}
