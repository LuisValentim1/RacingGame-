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
        public float speed = 5;
    
        // Methods
        protected override void OnUpdate() {
            // transform.position = Vector3.Lerp(,)

            if (player != null) {
                Vector3 newPos = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z);
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
            }
        }

        public void SetPlayerTarget(Transform transform) {
            player = transform;
        }
    }
}