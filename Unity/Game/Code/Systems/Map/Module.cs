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

        public GameObject GetRandomModuleCompatible() {
            return moduleConfiguration.modulesCompatible[UnityEngine.Random.Range(0, moduleConfiguration.modulesCompatible.Length)];
        }

        public Vector2 GetToNewPosition() {
            return new Vector2(transform.position.x + moduleConfiguration.to_direction.x * moduleConfiguration.size, transform.position.y + moduleConfiguration.to_direction.y * moduleConfiguration.size);
        }

        // Structs 
        [Serializable]       
        public struct ModuleConfiguration {
            public GameObject[] modulesCompatible;
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