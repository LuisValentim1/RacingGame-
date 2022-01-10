using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JamCat.Cameras;
using JamCat.Players;

namespace JamCat
{
    public class ManagerServer : MonoBehaviour
    {
        // Static
        public static ManagerServer instance;
        public static ManagerServer Get() { return instance; }

        // Variables
        public NetworkManager networkManager;

        // Methods -> Standard
        private void Awake() {
            instance = this;
        }

        private void Start() {
            
        }

        private void Update() {
            if (networkManager.IsClient == true && networkManager.IsServer == true) {
                print("Connections: " + networkManager.ConnectedClients.Count);
            } 
        }

        // Methods -> Public
        public void StartHost() {
            networkManager.StartHost();
            // SysPlayerServer.Get().CreateLocalPlayer();
            SysCamera.Get().SetPlayerTarget(SysPlayerServer.Get().GetLastPlayerCreated());
            // uNetTransport.StartServer();
        }

        public void StartClient() {
            networkManager.StartClient();
            // SysPlayerServer.Get().CreateLocalPlayer();
            // uNetTransport.StartClient();
        }

        public void InitServer() {
            // uNetTransport.Initialize();
        }

        public void DisconnectServer() {
            networkManager.Shutdown();
            // uNetTransport.Shutdown();
        }
    }
} 