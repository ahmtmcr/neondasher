using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformMovement : MonoBehaviour
{

    [SerializeField] Transform PlatformTransform;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
           other.transform.parent = PlatformTransform;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            other.transform.parent = null;
        }
    }
 
    

}
