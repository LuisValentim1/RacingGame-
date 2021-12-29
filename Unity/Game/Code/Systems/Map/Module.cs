using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CatJam.Players;

namespace CatJam.Map {
    public class Module : MonoBehaviour {

        // Variables
        public SquareConfiguration squareConfiguration;
        public ModuleConfiguration moduleConfiguration;
        public BuildingPositions[] buildingPositions;
        public Sprites sprites;

        public int moduleID;
        public bool playerWasInside;
        public bool noBackground = true;

        // Methods -> Standard
        private void Reset() {
            
        }
         
        private void OnTriggerEnter2D(Collider2D other) {

            // Create new module and destroy last
            if (other.tag == "Player" && playerWasInside == false) {
                if (moduleConfiguration.isFinishLine == true) {
                    GeneralMethods.CallFinish();
                } else {
                    other.GetComponent<TopDownCarController>().inModule = moduleID;
                    Generator.Get().DeleteLastModule(gameObject);

                    if (moduleID + 1 != Generator.Get().modulesQuantity)
                        Generator.Get().GenerateModule(moduleID + 1);
            
                    playerWasInside = true;                
                }
            }     
        }

        public void Generate(int moduleID) {
            this.moduleID = moduleID;
            Sprite randomSprite = GetRandomSpriteRoad();
            sprites.spriteRendererRoad.sprite = randomSprite;
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
            float prob = (float)Generator.Get().r.NextDouble() * 100;

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

        public Sprite GetRandomSpriteRoad() {
            return sprites.spritesRoad[UnityEngine.Random.Range(0, sprites.spritesRoad.Length)];
        }

        // Structs 
        [Serializable]       
        public struct ModuleConfiguration {
            public GameObject[] modules;
            public GameObject[] alternativeModules;
            public float[] probability;

            public GameObject moduleFinish;
            public GameObject alternativeFinish;

            public Vector2 from_direction;
            public Vector2 to_direction;
            public float size;

            public bool isStraight;
            public bool isStartingLine;
            public bool isFinishLine;
        }

        [Serializable]
        public struct SquareConfiguration {
            public int squares_rows_quant;
            public int squares_collumns_quant;
            public Square[,] squares;
        }

        [Serializable]
        public struct Sprites {
            public SpriteRenderer spriteRendererRoad;
            public Sprite[] spritesRoad;
        }

        [Serializable]
        public struct BuildingPositions {
            public float[] xs;
            public float[] ys;
        }
    }
}