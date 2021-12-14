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

        [Header("Configurations")]
        public int modulesQuantity = 100;
        public int generateQuantity = 1;
        public int deleteQuantity = 2;
        public GameObject[] startModules;

        public GameObject[] backgroundPrefabs = new GameObject[2];
        public Vector3[] cornerOffsets = {new Vector3(), new Vector3(), new Vector3(), new Vector3()};

        public System.Random r;

        [Header("Runtime - Test")]
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
            generateBuildings(newObj);

            // Currect Module Number - Array
            currentModuleArray++;
            if (currentModuleArray >= deleteQuantity + generateQuantity)
                currentModuleArray = 0;
        }

        public void DeleteLastModule(GameObject module) {
            Destroy(arrayModules[currentModuleArray]);
        }


        public void generateBuildings(GameObject mod){ 
            Vector3 pos = mod.transform.position;
            // Debug.Log(pos);
            if(mod.GetComponent<Module>().noBuildings){
                for(int i = 0; i<4; i++){
                    Vector3 b_pos = pos + cornerOffsets[i];
                    GameObject b = Instantiate (backgroundPrefabs[r.Next(0,2)], b_pos, transform.rotation, mod.transform);
                    mod.GetComponent<Module>().noBuildings = false;
                    // Debug.Log(b_pos);
                }
            }
        }
        

        public void RestartMap() {
            for (int i = 0; i < arrayModules.Length; i++) {
                if (arrayModules[i] != null)
                    Destroy(arrayModules[i]);
                arrayModules[i] = null;
            }
        }

        public Module GetInitialModule() {
            for (int i = 0; i < arrayModules.Length; i++)
                if (arrayModules[i] != null)
                    if (arrayModules[i].GetComponent<Module>().moduleConfiguration.isStartingLine == true)
                        return arrayModules[i].GetComponent<Module>();
            return null;
        }

        public float GetInitialPlayerRotation() {
            Module firstModule = GetInitialModule();
            if (firstModule != null) {
                if (firstModule.moduleConfiguration.to_direction == new Vector2(0, 1)){
                    return 0;
                } else if (firstModule.moduleConfiguration.to_direction == new Vector2(1, 0)) {
                    return 270;
                } else if (firstModule.moduleConfiguration.to_direction == new Vector2(0, -1)) {
                    return 180;
                } else if (firstModule.moduleConfiguration.to_direction == new Vector2(-1, 0)) {
                    return 90;
                }
            }

            return 0;
        }
    }
}
