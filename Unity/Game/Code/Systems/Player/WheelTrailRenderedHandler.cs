using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatJam.Player 
{
    public class WheelTrailRenderedHandler : MonoBehaviour
    {
        // Variables        
        TopDownCarController topDownCarController;
        TrailRenderer trailRenderer;

        // Methods
        public void OnAwake() {
            topDownCarController = GetComponentInParent<TopDownCarController>();
            trailRenderer = GetComponent<TrailRenderer>();
            trailRenderer.emitting = false; 
        }

        public void OnUpdate() {
            trailRenderer.emitting = topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBreaking);
        }
    }
}