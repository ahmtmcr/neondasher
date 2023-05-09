using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFallenPlayer : MonoBehaviour
{
   
   [SerializeField] Transform respawnPosition;
   
   private void OnTriggerEnter(Collider other) 
   {
       
       if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
       {
        
            other.gameObject.transform.position = respawnPosition.position;
          
       }
    }

}
