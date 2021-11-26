using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CatJam.UI 
{
    public class Window_Fade : Window
    {
        // Variables -> Instance
        public static Window_Fade instance;
        public static Window_Fade Get() { return instance; }

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