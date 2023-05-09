using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStartEffector : MonoBehaviour
{
   
    private SoundController soundController;

    
    public bool isEffected;
    
   void Start()
   {
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
   }
   
   private void OnTriggerEnter(Collider other) 
   {
       if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            soundController.PlayPlatformButtonPressedAudio();
            isEffected = true;
        }
   }

   private void OnTriggerExit(Collider other) 
   {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            isEffected = false;
        }
   }
}
