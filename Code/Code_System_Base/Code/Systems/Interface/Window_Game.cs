using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CatJam.UI 
{
    public class Window_Gade : Window
    {
        // Variables -> Instance
        public static Window_Gade instance;
        public static Window_Gade Get() { return instance; }

        // Variables
        public int currenct_character;

        // Methods -> Override
        protected override void OnAwakeWindow() {
            instance = this;
        }

        protected override void OnUpdateWindow() {

        }

        protected override void OnOpenWindow() {

        }

        protected override void OnCloseWindow() {

        }

        // Methods -> Public
        public void Configure() {
            
        }
    }
}