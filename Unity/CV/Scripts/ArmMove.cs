using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(Vector3 pos){
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 5);
    }
}
