using System.Collections.Generic;
using UnityEngine;
using JamCat.Players;
using Unity.Netcode;
using Unity.Netcode.Samples;

namespace JamCat.Players
{
    public class SysPlayerServer : NetworkBehaviour 
    {
        // Instance
        public static SysPlayerServer instance;
        public static SysPlayerServer Get() { return instance; }


        // Variables
        [Header("Configuration")]
        public GameObject prefabLocalPlayer;

        [Header("Run-Time only")]
        public List<Player> onlinePlayers;

        // Methods -> Standard
        public void OnAwake() {
            instance = this;
            onlinePlayers = new List<Player>();
        }

        public void OnUpdate() {
            for (int i = 0; i < onlinePlayers.Count; i++)
                onlinePlayers[i].OnUpdate();
        }

        public void OnRestart() {
            onlinePlayers = new List<Player>();

          //  for (int i = 0; i < onlinePlayers.Count; i++)
          //      onlinePlayers[i].Restart();
        }

        // Methods -> Get
        public Player GetLastPlayerCreated() {
            return onlinePlayers[onlinePlayers.Count - 1];
        }

        // Methods -> Server
        /*
        [ServerRpc]
        public void CreateLocalPlayerServerRpc() {
            GameObject newObj = Instantiate(prefabLocalPlayer, Vector3.zero, Quaternion.identity);
            newObj.GetComponent<NetworkObject>().Spawn();
        }   

        private void InstantiatePlayer() {
            GameObject newGameObject = Instantiate(prefabLocalPlayer, Vector3.zero, Quaternion.identity);
            newGameObject.GetComponent<NetworkObject>().Spawn();
            Player newPlayer = newGameObject.GetComponent<Player>();
            onlinePlayers.Add(newPlayer);
        }
        */
    }
}