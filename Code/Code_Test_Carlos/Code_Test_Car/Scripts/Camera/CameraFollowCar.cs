using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
  
    void Update () 
    {
        transform.position = new Vector3 (player.transform.position.x , player.transform.position.y , transform.position.z); // Camera follows the player with specified offset position
    }
}
