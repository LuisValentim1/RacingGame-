using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JamCat.UI 
{
    public class Window_Normal : Window
    {
        // Variables -> Instance
        public static Window_Normal instance;
        public static Window_Normal Get() { return instance; }

        // Variables

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnOpenWindow() {

        }
        
        protected override void OnCloseWindow() {

        }

        protected override void OnUpdateWindow() {

        }

        // Methods -> Public
    }
}