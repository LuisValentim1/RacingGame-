using System;
using System.Collections;
using UnityEngine;

namespace CatJam.Map {
    public class Module : MonoBehaviour {

        // Variables
        public int squares_rows_quant;
        public int squares_collumns_quant;
        public Square[,] squares;
        public int squares_size;

        public Compatible[] compatibles;

        // Methods -> Standard
        private void Reset() {
            compatibles = new Compatible[4];
            compatibles[0] = new Compatible(0, 2, false);
            compatibles[1] = new Compatible(90, 2, false);
            compatibles[2] = new Compatible(180, 2, false);
            compatibles[3] = new Compatible(270, 2, false);
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
        public bool IsCompatible (Module with_module) {
            bool is_compatible = false;
          
            for (int i = 0; i < 4; i++) {
               
               /*
                if (compatibles[i].direction == with_module.compatibles[i].direction - 180) {
                    if (compatibles[i].road_size == with_module.compatibles[i].road_size) {
                        if (compatibles[i].road_exists == true) {
                            
                        }
                    }
                }
                */
            }
            return is_compatible;         
        }

        // Structs
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
    }
}