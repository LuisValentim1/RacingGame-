using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamCat.Players
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
            trailRenderer.emitting = topDownCarController.isTireScreeching(out float lateralVelocity, out bool isBreaking);
        }
    }
}