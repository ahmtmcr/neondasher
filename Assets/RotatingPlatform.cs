using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed = 0.02f;
    public float rotationDirection = 1;
    void FixedUpdate()
    {
        
        transform.Rotate(20*Time.deltaTime, 0, 0);
    }
}
