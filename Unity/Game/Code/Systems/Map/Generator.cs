using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatJam.Map {
    public class Generator : MonoBehaviour {

        // Instance
        public static Generator instance;
        public static Generator Get() { return instance; }

        // Variables 
        public int modules_quantity = 100;

        private List<Module> list_modules;
        private Module last_module;

        // Methods -> Standard
        public void OnAwake() {
            instance = this;
        }

        public void OnStart() {

        }

        public void OnUpdate() {

        }

        // Methods -> Public
        public void GenerateMap() {
            for (int i = 0; i < modules_quantity; i++) {
                Generate_Module();
            }
        }

        private void Generate_Module() {
            Module new_module = new Module();

            list_modules.Add(new_module);
            last_module = new_module;
        }

        public void RestartMap() {
            
        }

        public void Undo() {

        }

    }
}
