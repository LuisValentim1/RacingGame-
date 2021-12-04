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
            /*
            compatibles = new Compatible[4];
            compatibles[0] = new Compatible(0, 2, false);
            compatibles[1] = new Compatible(90, 2, false);
            compatibles[2] = new Compatible(180, 2, false);
            compatibles[3] = new Compatible(270, 2, false);
            */
        }

        // Methods -> Public
        public bool IsSquareEmpty() {
            return false;
        }

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

        // NOT FINISHED
        /*
        public bool IsCompatible (Module with_module) {
            bool is_compatible = false;

            List<bool> exists = new List<bool>();
            List<int> directions = new List<int>();

            for (int i = 0; i < 4; i++) {
                for (int y = 0; y < 4; y++) {
                    
                    // Se os modulos não tiverem o mesmo lado
                    if (i != y) {
                        // Se existe estrada na direção dada
                        if (compatibles[i].road_exists == true) {
                            if (with_module.compatibles[y].road_exists == true) {

                            }
                        }
                    }
                    // Exemplo de direção
                    // 4+4 = 8-4 = 4
                    // 4+0 = 4-4 = 0 
                    if (compatibles[i].road_exists == true) {
                        if () {

                        }
                        int dir = compatibles[i].direction + with_module.compatibles[i].direction;
                        if (dir >= 4)
                            dir -= 4;
                    }

                /*
                    if (compatibles[i].direction == with_module.compatibles[i].direction - 180) {
                        if (compatibles[i].road_size == with_module.compatibles[i].road_size) {
                            if (compatibles[i].road_exists == true) {
                                
                            }
                        }
                    }

                }
            }
            return is_compatible;         
        }
         */

        public GameObject GetRandomModuleCompatible() {
            return moduleConfiguration.modulesCompatible[UnityEngine.Random.Range(0, moduleConfiguration.modulesCompatible.Length)];
        }

        public Vector2 GetToNewPosition() {
            return new Vector2(moduleConfiguration.to_direction.x * moduleConfiguration.square_size, moduleConfiguration.to_direction.y * moduleConfiguration.square_size);
        }

        // Structs        
        public struct ModuleConfiguration {
            public GameObject[] modulesCompatible;
            public Vector2 from_direction;
            public Vector2 to_direction;
            public float square_size;
        }

        public struct SquareConfiguration {
            public int squares_rows_quant;
            public int squares_collumns_quant;
            public Square[,] squares;
            public int squares_size;
        }
        /*
        public struct Compatible {
            public Compatible (int direction, int road_size, bool road_exists) {
                this.direction = direction;
                this.road_size = road_size;
                this.road_exists = road_exists;
            }

            public int direction;
            public int road_size;
            public bool road_exists;
        }
        */
    }
}