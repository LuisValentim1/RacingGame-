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
        public GameObject[] start_modules;

        public System.Random r;

        private List<GameObject> list_modules;
        private GameObject last_module;


        // Methods -> Standard
        public void OnAwake() {
            instance = this;
            r = new System.Random(DateTime.Now.Day);
        }

        public void OnStart() {
            list_modules = new List<GameObject>();
        }

        public void OnUpdate() {

//            if (Input.GetKeyDown(KeyCode.T)) {
  //              GenerateMap();
    //        }
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
                new_module = start_modules[UnityEngine.Random.Range(0, start_modules.Length)];
            } else if (number == modules_quantity - 1) {
                new_module = last_module.GetComponent<Module>().GetFinish();
            } else {
                new_module = last_module.GetComponent<Module>().GetRandomModule();
            }

            GameObject newObj = null;
            if (number == 0) {
                newObj = Instantiate(new_module, transform);
                newObj.transform.position = Vector3.zero;
            } else {
                newObj = Instantiate(new_module, transform);
                newObj.transform.position = last_module.GetComponent<Module>().GetToNewPosition();
            }

            list_modules.Add(newObj);
            last_module = newObj;
        }

        public void RestartMap() {
            for(int i = 0; i < list_modules.Count; i++) {
                Destroy(list_modules[i].gameObject);
            }

            list_modules = new List<GameObject>();
        }



        public void Undo(int steps) {

        }

    }
}
