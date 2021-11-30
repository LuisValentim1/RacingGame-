using System;
using System.Collections;
using UnityEngine;

namespace CatJam.Map {
    public class SysMap : Sys {
        
        // Instance
        public static SysMap instance;
        public static SysMap Get() { return instance; }

        // Variables -> Public
        public Generator generator;

        // Methods -> Override
        protected override void OnAwake() {
            generator.OnAwake();
        }

        protected override void OnStart() {
            generator.OnStart();
        }

        protected override void OnUpdate() {
            generator.OnUpdate();
        }

        // Methods -> Public
        public void GenerateMap() {
            generator.GenerateMap();
        }

        public void RestartMap() {
            generator.RestartMap();
        }
    }
}