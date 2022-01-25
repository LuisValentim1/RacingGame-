using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JamCat.Map;

namespace JamCat.Map 
{
    public class GeneratorClient : MonoBehaviour 
    {
        // Instance
        public static GeneratorClient instance;
        public static GeneratorClient Get() { return instance; }

        // Variables 
        [Header("Configurations")]
        public GameObject[] allModules;
        public GameObject[] allElements;
        public int generateQuantity = 1;
        public int deleteQuantity = 2;

        [Header("Runtime - Test")]
        public int modulesCreated;
        public int currentModuleArray;
        public GameObject[] arrayModules;
        public GameObject lastModule;


        // Methods -> Standard
        public void OnAwake() {
            instance = this;
            RestartMap();
        }

        public void OnStart() {

        }

        public void OnUpdate() {

        }

        // Methods -> Public
        public GameObject FindModuleObj(int moduleID) {
            for (int i = 0; i < allModules.Length; i++)
                if (allModules[i].GetComponent<Module>().moduleID == moduleID)
                    return allModules[i];
            return null;
        }

        public GameObject FindElementObj(int elementID) {
            for (int i = 0; i < allElements.Length; i++)
                if (allElements[i].GetComponent<Element>().elementID == elementID)
                    return allElements[i];
            return null;
        }

        public Module GenerateModule(int elementID, Vector3 pos) {
            if (NetworkManager.Singleton.IsServer == true)
                return null;

            // Instantiate
            GameObject newObj = Instantiate(FindModuleObj(elementID), transform);
            newObj.transform.position = pos;
            Module module = newObj.GetComponent<Module>();
            module.moduleNumber = modulesCreated;

            // Set Model Created
            arrayModules[currentModuleArray] = newObj;
            lastModule = newObj;

            // Currect Module Number - Array
            modulesCreated++;
            currentModuleArray++;
            if (currentModuleArray >= deleteQuantity + generateQuantity)
                currentModuleArray = 0;

            return newObj.GetComponent<Module>();
        }

        public void GenerateElementsInModule(int moduleNumber, ModuleClient.SerializeElements elements) {
            Module module = GetModuleCreated(moduleNumber);
            
            for (int i = 0; i < elements.elementsID.Length; i++) {
                print("Element ID: " + elements.elementsID[i]);
                GameObject objFound = FindElementObj(elements.elementsID[i]);

                if (objFound.GetComponent<Element>().dontSync == true)
                    continue;

                GameObject newObj = Instantiate(objFound, elements.elementsPos[i], elements.elementsRot[i], module.transform);
            }
        }

        public void DeleteLastModule() {
            if (NetworkManager.Singleton.IsServer == true)
                return;

            Destroy(arrayModules[currentModuleArray]);
        }

        public void RestartMap() {
            modulesCreated = 0;
            currentModuleArray = 0;
            lastModule = null;
        
            for (int i = 0; i < arrayModules.Length; i++) {
                if (arrayModules[i] != null)
                    Destroy(arrayModules[i]);
                arrayModules[i] = null;
            }

            arrayModules = new GameObject[generateQuantity + deleteQuantity];
        }

        public Module GetModuleCreated(int moduleNumber) {
            for (int i = 0; i < arrayModules.Length; i++)
                if (arrayModules[i] != null)
                    if (arrayModules[i].GetComponent<Module>().moduleNumber == moduleNumber)
                        return arrayModules[i].GetComponent<Module>();
            return null;
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