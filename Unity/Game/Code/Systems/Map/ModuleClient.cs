using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JamCat.Players;
using Unity.Netcode;

namespace JamCat.Map 
{
    public class ModuleClient : NetworkBehaviour 
    {

        public SerializeElements GetSerializeElements() {
            SerializeElements serializeElements = new SerializeElements();
            Element[] elements = GetComponentsInChildren<Element>();
            serializeElements.elementsID = new int[elements.Length];
            serializeElements.elementsPos = new Vector3[elements.Length];
            serializeElements.elementsRot = new Quaternion[elements.Length];
            for (int i = 0; i < elements.Length; i++) {
                serializeElements.elementsID[i] = elements[i].elementID;
                serializeElements.elementsPos[i] = elements[i].transform.position;
                serializeElements.elementsRot[i] = elements[i].transform.rotation;
            }        
            return serializeElements;
        }

        public struct SerializeElements : INetworkSerializable {
            public int[] elementsID;
            public Vector3[] elementsPos;
            public Quaternion[] elementsRot;
            
            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
                serializer.SerializeValue(ref elementsID);
                serializer.SerializeValue(ref elementsPos);
                serializer.SerializeValue(ref elementsRot);
            }
        }

    }
}