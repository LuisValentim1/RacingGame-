using System;
using System.Collections;
using UnityEngine;

namespace JamCat.Map {
    public class SysMap : Sys {
        
        // Instance
        public static SysMap instance;
        public static SysMap Get() { return instance; }

        // Variables -> Public
        public GeneratorServer generatorServer;
        public GeneratorClient generatorClient;

        // Methods -> Override
        protected override void OnAwake() {
            instance = this;
            generatorServer.OnAwake();
            generatorClient.OnAwake();
        }

        protected override void OnStart() {
            generatorServer.OnStart();
            generatorClient.OnStart();
        }

        protected override void OnUpdate() {
            generatorServer.OnUpdate();
            generatorClient.OnUpdate();
        }

        // Methods -> Public
        public void GenerateMap() {
            generatorServer.GenerateMap();
        }

        public void RestartMap() {
            generatorServer.RestartMap();
            generatorClient.RestartMap();
        }
    }
}