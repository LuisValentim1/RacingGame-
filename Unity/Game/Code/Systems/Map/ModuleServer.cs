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
    public class ModuleServer : NetworkBehaviour 
    {
        

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