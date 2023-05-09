using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3DContoller : MonoBehaviour
{
    
    public Camera cam;
    public Vector3 offset;
    public float smoothSpeed = 5f;
    void FixedUpdate()
    {
        Vector3 target = new Vector3(transform.position.x, 0, transform.position.z) + offset;
        Vector3 smoothTarget = Vector3.Lerp(cam.transform.position, target, smoothSpeed * Time.fixedDeltaTime);
        cam.transform.position = smoothTarget;
    }
}
