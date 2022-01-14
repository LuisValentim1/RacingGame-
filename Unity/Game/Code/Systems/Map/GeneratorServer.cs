using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JamCat.Map;

namespace JamCat.Map {
    public class GeneratorServer : MonoBehaviour {

        // Instance
        public static GeneratorServer instance;
        public static GeneratorServer Get() { return instance; }

        // Variables 
        public bool gerarTudo;

        [Header("Configurations")]
        public int modulesQuantity = 100;
        public int numberOfElementsPerModule;
        public int generateQuantity = 1;
        public int deleteQuantity = 2;
        public GameObject[] startModules;

        public GameObject[] backgroundPrefabs;
        public ElementPrefabs[] elementPrefabs;

        public System.Random r;

        [Header("Runtime - Test")]
        public int modulesCreated;
        public int currentModuleArray;
        public GameObject[] arrayModules;
        public GameObject lastModule;


        // Methods -> Standard
        public void OnAwake() {
            instance = this;

            UnityEngine.Random.InitState(DateTime.Now.Second);
            r = new System.Random(DateTime.Now.Second);

            RestartMap();
        }

        public void OnStart() {

        }

        public void OnUpdate() {

        }

        // Methods -> Public
        public void GenerateMap() {
            if (NetworkManager.Singleton.IsServer == false)
                return;

            if (gerarTudo == true) {
                for (int i = 0; i < modulesQuantity; i++) {
                    GenerateModule();
                }
            } else {
                for (int i = 0; i < generateQuantity; i++) {
                    SysMultiplayer.Get().multiplayerMethods.GenerateModuleServerRpc();
                }
            }
        }

        public Module GenerateModule() {
            if (NetworkManager.Singleton.IsServer == false)
                return null;

            if (modulesCreated == modulesQuantity) 
                return null;

            GameObject new_module;

            // Chooses the next Module
            if (modulesCreated == 0) {
                new_module = startModules[UnityEngine.Random.Range(0, startModules.Length)];
            } else if (modulesCreated == modulesQuantity - 1) {
                new_module = lastModule.GetComponent<Module>().GetFinish();
            } else {
                new_module = lastModule.GetComponent<Module>().GetRandomModule();
            }

            GameObject newObj = null;
            if (modulesCreated == 0) {
                newObj = Instantiate(new_module, transform);
                newObj.transform.position = Vector3.zero;
            } else {
                newObj = Instantiate(new_module, transform);
                newObj.transform.position = lastModule.GetComponent<Module>().GetToNewPosition();
            }

            Module module = newObj.GetComponent<Module>();
            module.Generate(modulesCreated);

            Module.RoadPosition[] freePositions = module.freeRoadPositions;
            // For track randomization purposes modules may spawn in places that don't match their standard orientation, a left->down curve may appear with top->left orientation
            // When this happens values based on module have to be altered to their alternative versions 
            // Road Element Generation is processed at this point for efficency reasons  
            if(intIsWithinRange(modulesQuantity - 1, 0, modulesCreated)){
                float moduleSize = module.moduleConfiguration.size;
                Vector2 from = new Vector2((lastModule.transform.position.x - newObj.transform.position.x) / moduleSize, (lastModule.transform.position.y - newObj.transform.position.y) / moduleSize);
                if(from != module.moduleConfiguration.from_direction){
                    module.moduleConfiguration.to_direction = module.moduleConfiguration.from_direction;
                    module.moduleConfiguration.moduleFinish = module.moduleConfiguration.alternativeFinish;
                    module.moduleConfiguration.modules = module.moduleConfiguration.alternativeModules;
                    for(int i = 0; i<freePositions.Length; i++){
                        freePositions[i].rotation = freePositions[i].altRotation;
                    }
                }
                generateRoadElements(newObj, freePositions, numberOfElementsPerModule);
            }
            generateBackground(newObj);


            // Set Model Created
            arrayModules[currentModuleArray] = newObj;
            lastModule = newObj;

            // Currect Module Number - Array
            modulesCreated++;
            currentModuleArray++;
            if (currentModuleArray >= deleteQuantity + generateQuantity)
                currentModuleArray = 0;

            return module;
        }


        public Module GetModuleCreated(int moduleNumber) {
            for (int i = 0; i < arrayModules.Length; i++)
                if (arrayModules[i] != null)
                    if (arrayModules[i].GetComponent<Module>().moduleNumber == moduleNumber)
                        return arrayModules[i].GetComponent<Module>();
            return null;
        }

        public void DeleteLastModule() {
            Destroy(arrayModules[currentModuleArray]);
            SysMultiplayer.Get().multiplayerMethods.DestroyLastModuleClientRpc();
        }

        //Method for background buildings placement
        //Get the module center position, add a semi random offset within certain intervals to assure buildings don't overlap, instatiate random building within calculated position, repeat for each of the six open positions in each module.
        //Intervals are defined within the module
        public void generateBackground(GameObject module){ 
            Vector3 pos = module.transform.position;
            if(module.GetComponent<Module>().noBackground){
                for(int i = 0; i<6; i++){
                    Module.BuildingPositions curPosition = module.GetComponent<Module>().buildingPositions[i];
                    Vector3 offset = new Vector3(FloatWithinInterval(r,curPosition.xs[1], curPosition.xs[0]), FloatWithinInterval(r, curPosition.ys[1], curPosition.ys[0]), -1);
                    Vector3 building_pos = pos + offset;
                    GameObject building = Instantiate(backgroundPrefabs[r.Next(0,2)], building_pos, transform.rotation, module.transform);
                }
                module.GetComponent<Module>().noBackground = false;
            }
        }

        //Method for road elements placement
        //Get the module center position, add a static offset, instatiate random element in the calculated position with an associated rotation, remove the offset used from possible offsets for following elements
        //Each module has an array of available offsets and rotations in which elements may spawn 
        public void generateRoadElements(GameObject module, Module.RoadPosition[] freeSpots, int numberOfElements){
            Vector3 pos = module.transform.position;
            Vector3 offset;
            int pos_index;
            Module.RoadPosition cur;
            int x = 0;
            if(module.GetComponent<Module>().noElements){
                if(module.GetComponent<Module>().moduleConfiguration.isStraight && r.Next(0,2) == 1){
                    pos_index = r.Next(0,2);
                    offset = new Vector3(freeSpots[pos_index].x, freeSpots[pos_index].y, -1);
                    GameObject element = Instantiate(elementPrefabs[2].prefabs[r.Next(0,2)], pos+offset, Quaternion.Euler(0, 0, freeSpots[pos_index].rotation), module.transform);
                    cur = freeSpots[pos_index];
                    if(module.GetComponent<Module>().moduleConfiguration.isHorizontal){
                        freeSpots = freeSpots.Where(e => !(e.y.Equals(cur.y))).ToArray();
                    }
                    else{
                        freeSpots = freeSpots.Where(e => !(e.x.Equals(cur.x))).ToArray();
                    }
                    x=1;
                }
                for(int i = 0 + x; i<numberOfElements; i++){
                    pos_index = r.Next(0,freeSpots.Length);
//                    print(module.GetComponent<Module>().moduleID);
                    offset = new Vector3(freeSpots[pos_index].x, freeSpots[pos_index].y, -1);
                    Vector3 element_pos = pos + offset;
                    int element_index = r.Next(0,2);
                    GameObject element = Instantiate(elementPrefabs[element_index].prefabs[r.Next(0,elementPrefabs[element_index].prefabs.Length)], element_pos, Quaternion.Euler(0, 0, freeSpots[pos_index].rotation), module.transform);
                    cur = freeSpots[pos_index];
                    freeSpots = freeSpots.Where(e => !(e.Equals(cur))).ToArray();
                }
                module.GetComponent<Module>().noElements = false;
            }
        } 

        // Efficent check if int is within a range, min and max not included 
        public Boolean intIsWithinRange(int max, int min, int x){
            int check = (max-x) * (min-x);
            if(check<0){
                return true;
            }
            return false;
        }

        // Get a float within an interval
        public float FloatWithinInterval(System.Random rng, float max, float min){
            double val = (rng.NextDouble() * Math.Abs(max - min) + min);
            return (float)val; 
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

        [Flags]
        public enum ElementType{
            Obstacle,
            Ramp,
            ManaStrip
        }

        [Serializable]
        public struct ElementPrefabs {
            public GameObject[] prefabs;
            public ElementType type;
        }
    }
}