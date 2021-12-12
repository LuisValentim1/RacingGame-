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
        public bool gerarTudo;

        public int modulesQuantity = 100;
        public int generateQuantity = 1;
        public int deleteQuantity = 2;
        public GameObject[] startModules;

        public System.Random r;

        public int currentModuleArray;
        public GameObject[] arrayModules;
        public GameObject lastModule;


        // Methods -> Standard
        public void OnAwake() {
            instance = this;
            r = new System.Random(DateTime.Now.Day);
            arrayModules = new GameObject[generateQuantity + deleteQuantity];
        }

        public void OnStart() {

        }

        public void OnUpdate() {

        }

        // Methods -> Public
        public void GenerateMap() {
            if (gerarTudo == true) {
                for (int i = 0; i < modulesQuantity; i++) {
                    GenerateModule(i);
                }
            } else {
                for (int i = 0; i < generateQuantity; i++) {
                    GenerateModule(i);
                }
            }
        }

        public void GenerateModule(int number) {
            GameObject new_module;

            // Chooses the next Module
            if (number == 0) {
                new_module = startModules[UnityEngine.Random.Range(0, startModules.Length)];
            } else if (number == modulesQuantity - 1) {
                new_module = lastModule.GetComponent<Module>().GetFinish();
            } else {
                new_module = lastModule.GetComponent<Module>().GetRandomModule();
            }

            GameObject newObj = null;
            if (number == 0) {
                newObj = Instantiate(new_module, transform);
                newObj.transform.position = Vector3.zero;
            } else {
                newObj = Instantiate(new_module, transform);
                newObj.transform.position = lastModule.GetComponent<Module>().GetToNewPosition();
            }

            newObj.GetComponent<Module>().moduleId = number;

            arrayModules[currentModuleArray] = newObj;
            lastModule = newObj;

            // Currect Module Number - Array
            currentModuleArray++;
            if (currentModuleArray >= deleteQuantity + generateQuantity)
                currentModuleArray = 0;
        }

        public void DeleteLastModule(GameObject module) {
            Destroy(arrayModules[currentModuleArray]);
        }

        

        public void RestartMap() {
            for (int i = 0; i < arrayModules.Length; i++) {
                if (arrayModules[i] != null)
                    Destroy(arrayModules[i]);
                arrayModules[i] = null;
            }
        }
    }
}
