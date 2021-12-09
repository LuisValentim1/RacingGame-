using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatJam.Map {
    public class Module : MonoBehaviour {

        // Variables
        public SquareConfiguration squareConfiguration;
        public ModuleConfiguration moduleConfiguration;

        //public Compatible[] compatibles;

        // Methods -> Standard
        private void Reset() {
            
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

            // Probabilty
            int module_number;
            int prob = Generator.Get().r.Next(0,100);

            if(prob < moduleConfiguration.probability[0])            {
                module_number = 0;
            }
            else if(prob < moduleConfiguration.probability[0] +  moduleConfiguration.probability[1])            {
                module_number = 1;
            }
            else{
                module_number = 2;
            }

            module = moduleConfiguration.modules[module_number];
            return module;
        }

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
        }

        [Serializable]
        public struct SquareConfiguration {
            public int squares_rows_quant;
            public int squares_collumns_quant;
            public Square[,] squares;
        }
    }
}