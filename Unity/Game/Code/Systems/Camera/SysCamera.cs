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
        public Camera[] cameras;
        public Camera currentCamera;
        public CameraBase currentCameraBase;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            SetCamera(0);
        }

        protected override void OnStart() {
            
        }

        protected override void OnUpdate() {
            currentCameraBase.UpdateCamera();
        }

        // Methods -> Public
        public void AutoConfigureCamera() {

        }

        public void SetCamera(int id) {
            for (int i = 0; i < cameras.Length; i++)
                cameras[i].gameObject.SetActive(false);
            cameras[id].gameObject.SetActive(true);
            currentCamera = cameras[id].GetComponent<Camera>();
            currentCameraBase = cameras[id].GetComponent<CameraBase>();
        }
    }
}
