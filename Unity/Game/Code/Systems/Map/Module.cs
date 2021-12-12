using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CatJam.Player;

namespace CatJam.Map {
    public class Module : MonoBehaviour {

        // Variables
        public SquareConfiguration squareConfiguration;
        public ModuleConfiguration moduleConfiguration;

        public int moduleId;
        public bool playerWasInside;
        public bool noBuildings = true;

        // Methods -> Standard
        private void Reset() {
            
        }
         
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player" && playerWasInside == false) {
                if (moduleConfiguration.isFinishLine == true) {
                    GeneralMethods.CallFinish();
                } else {
                    other.GetComponent<TopDownCarController>().inModule = moduleId;
                    Generator.Get().DeleteLastModule(gameObject);

                    if (moduleId + 1 != Generator.Get().modulesQuantity)
                        Generator.Get().GenerateModule(moduleId + 1);
            
                    playerWasInside = true;                
                }
            }     
        }

        // Methods -> Public

        // Methods -> Get
        public Square GetEmptySquare() {
            return null;
        }

        public int CalculateAngle() {
            return 0;
        }

        public int GetInverseCompatible() {
            return 0;
        }

        public GameObject GetFinish() {
            return moduleConfiguration.moduleFinish;
        }

        public GameObject GetRandomModule() {
            GameObject module = null;

            int module_number = 0;
            int prob = Generator.Get().r.Next(0,100);

            float increment = 0;
            for (int i = 0; i < moduleConfiguration.modules.Length; i++) {
                increment += moduleConfiguration.probability[i];
                if (prob < increment) {
                    module_number = i;
                    break;
                }
            }

            module = moduleConfiguration.modules[module_number];
            return module;
        }

        // Return the position for the generation of a New Module
        public Vector2 GetToNewPosition() {
            return new Vector2(transform.position.x + moduleConfiguration.to_direction.x * moduleConfiguration.size, 
                transform.position.y + moduleConfiguration.to_direction.y * moduleConfiguration.size);
        }

        // Structs 
        [Serializable]       
        public struct ModuleConfiguration {
            public GameObject[] modules;
            public int[] probability;

            public GameObject moduleFinish;
            public Vector2 from_direction;
            public Vector2 to_direction;
            public float size;

            public bool isFinishLine;
        }

        [Serializable]
        public struct SquareConfiguration {
            public int squares_rows_quant;
            public int squares_collumns_quant;
            public Square[,] squares;
        }
    }
}