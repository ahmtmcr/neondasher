using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIGetCamera : MonoBehaviour
{
    private Canvas canvas;
    public Camera cam;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = cam;
    }

  
}
