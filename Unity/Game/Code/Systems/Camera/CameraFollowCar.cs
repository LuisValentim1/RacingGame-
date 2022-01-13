using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JamCat.Cameras
{
    public class CameraFollowCar : CameraBase
    {
        // Variables
        public Transform player;
        public Vector3 offset;
    
        // Methods
        protected override void OnUpdate() {
            // transform.position = Vector3.Lerp(,)

            if (player != null)
                transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
        }

        public void SetPlayerTarget(Transform transform) {
            player = transform;
        }
    }
}