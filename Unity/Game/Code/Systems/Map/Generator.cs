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
        public GameObject start_module;
        public GameObject finish_module;


        private List<GameObject> list_modules;
        private GameObject last_module;

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
                Generate_Module(i);
            }
        }

        private void Generate_Module(int number) {
            GameObject new_module;

            // Chooses the next Module
            if (number == 0) {
                new_module = start_module;
            } else if (number == modules_quantity) {
                new_module = finish_module;
            } else {
                new_module = last_module.GetComponent<Module>().GetRandomModuleCompatible();
            }

            GameObject newObj = null;
            if (number == 0) {
                newObj = Instantiate(new_module, Vector2.zero, Quaternion.identity, transform);
            } else {
                newObj = Instantiate(new_module, last_module.GetComponent<Module>().GetToNewPosition(), Quaternion.identity, transform);
            }

            list_modules.Add(newObj);
            last_module = newObj;
        }

        public void RestartMap() {
            
        }



        public void Undo(int steps) {

        }

    }
}
