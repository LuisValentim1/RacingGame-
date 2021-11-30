using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatJam.UI 
{
    abstract public class UI : MonoBehaviour
    {
        // Variables

        // Methods -> Abstract
        abstract protected void OnAwake();
        abstract protected void OnUpdate();
        abstract protected void OnOpen();
        abstract protected void OnClose();

        // Methods -> Standard
        public void AwakeUI() {
            OnAwake();
        }

        public void UpdateUI() {
            OnUpdate();
        }

        public void OpenUI() {
            OnOpen();
        }

        public void CloseUI() {
            OnClose();
        }
    }
}