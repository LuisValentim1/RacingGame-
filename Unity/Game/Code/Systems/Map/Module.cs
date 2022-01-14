using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JamCat.Players;
using Unity.Netcode;

namespace JamCat.Map {
    public class Module : MonoBehaviour {

        // Variables
        public int moduleID;
        public SquareConfiguration squareConfiguration;
        public ModuleConfiguration moduleConfiguration;
        public BuildingPositions[] buildingPositions;
        public RoadPosition[] freeRoadPositions;
        public Sprites sprites;

        public int moduleNumber;
        public bool playerWasInside;
        public bool noBackground = true;
        public bool noElements = true;

        public Element[] elements;

        // Methods -> Standard
        private void Reset() {
            
        }

        public void Generate(int moduleNumber) {
            this.moduleNumber = moduleNumber;
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
            float prob = (float)GeneratorServer.Get().r.NextDouble() * 100;

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
            public bool isHorizontal;
            public bool isStartingLine;
            public bool isFinishLine;
        }

        //Is this ever used?
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

        [Serializable]
        public struct RoadPosition {
            public float x;
            public float y;
            public int rotation;
            public int altRotation;
        }
    }
}