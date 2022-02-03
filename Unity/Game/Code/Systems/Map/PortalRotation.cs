using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotation : MonoBehaviour
{

    public GameObject layer1;
    public GameObject layer2;
    public GameObject layer3;
    public GameObject layer4;

    public float layer1Speed = 0.3f;
    public float layer2Speed = 0.6f;
    public float layer3Speed = 0.8f;
    public float layer4Speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        layer1.transform.Rotate(0.0f, 0.0f, 1.0f * layer1Speed);
        layer2.transform.Rotate(0.0f,0.0f, 1.0f * layer2Speed);
        layer3.transform.Rotate(0.0f, 0.0f, 1.0f * layer3Speed);
        layer4.transform.Rotate(0.0f,0.0f, 1.0f * layer4Speed);
    }
}
