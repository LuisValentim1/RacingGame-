using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JamCat.UI;

public class ServerTest : NetworkBehaviour
{
    public bool isReady;
    public int playersMaxQuantity;

    public NetworkObject networkObject;

    public void Restart() {

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            if (networkObject.IsLocalPlayer == true) {
                MessagePingRequestServerRpc("Works!!");


            }
        }
    }

    public void MessagePing(string text) {
        if (NetworkManager.IsServer == true) {
            Debug.Log(text);
        } else {
            MessagePingRequestServerRpc(text);
        }
    }


    [ClientRpc]
    public void CallCaracterSelectionClientRpc() {
        Window_Lobby.Get().CloseWindow(0.3f, 0);
        Window_CharacterSelection.Get().OpenWindow(0.3f, 0.3f);
    }


    [ServerRpc]
    public void MessagePingRequestServerRpc(string text) {
        Debug.Log("1 ---> Server");
        MessagePingClientRpc(text);
    }

    [ClientRpc]
    public void MessagePingClientRpc(string test) {
        Debug.Log("2 ---> Client");
        Debug.Log(test);

        
    }

}
