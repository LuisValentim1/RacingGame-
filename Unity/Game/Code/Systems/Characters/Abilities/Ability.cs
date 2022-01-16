using System;
using System.Collections;
using UnityEngine;
using Unity.Netcode;
using JamCat.Players;
using JamCat.Multiplayer;

namespace JamCat.Characters 
{
    abstract public class Ability : MonoBehaviour 
    {
        // Variables -> Public
        [Header("Configurable")]
        public int id;
        public float manaNeeded;
        public float cooldown;



        private float curCooldown;
        protected Character character;

        // Methods -> Abstract
        protected abstract void OnAwake();
        protected abstract void OnStart();
        protected abstract void OnUpdate();
        protected abstract void OnUse();
        protected abstract string[] OnSendInfo();
        protected abstract void OnGetInfo(string[] info);

        // Methods -> Public
        public void AwakeAbility() {
            this.character = GetComponentInParent<Character>();
            OnAwake();
        }

        public void StartAbility() {
            OnStart();
        }

        public void UpdateAbility() {
            OnUpdate();

            if (curCooldown > 0)
                curCooldown -= Time.deltaTime;
        }

        public void UseAbility() {
            if (Manager.Get().hacks == false) {
                if (curCooldown > 0)
                    return;

                if (character.curMana < manaNeeded)
                    return;
            }

            curCooldown = cooldown;
            character.RemoveMana(manaNeeded);
            SendInfoToServer();
            OnUse();
        }

        public void SendInfoToServer() {
            MultiplayerMethods.Get().UseAbilityServerRpc(SysPlayer.Get().localPlayerID, id);
        }

        public void ReceiveInfoFromServer() {
            OnUse();
        }


/*
        public SerializeElements GetSerializeElements() {
            SerializeElements serializeElements = new SerializeElements();
            Element[] elements = GetComponentsInChildren<Element>();
            serializeElements.elementsID = new int[elements.Length];
            serializeElements.elementsPos = new Vector3[elements.Length];
            serializeElements.elementsRot = new Quaternion[elements.Length];
            for (int i = 0; i < elements.Length; i++) {
                // print(elements[i].name);
                serializeElements.elementsID[i] = elements[i].elementID;
                serializeElements.elementsPos[i] = elements[i].transform.position;
                serializeElements.elementsRot[i] = elements[i].transform.rotation;
            }        

            return serializeElements;
        }

        public struct SerializeElements : INetworkSerializable {
            public Quaternion[] elementsRot;
            
            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
                if (abilityInfo != null)
                    serializer.SerializeValue(ref abilityInfo);

                serializer.SerializeValue(ref elementsRot);
            }
        }
*/
    }
}