using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AddGlow : MonoBehaviour
{
   float brightness = 0.6f;
   float timer = 0f;
   float timer2 = 0f;
   float time = 1f;
  

   [SerializeField] GameObject[] blocks;
   [SerializeField] Material[] mat;


   bool playerOnTrigger = false;
   
   void Start()
   {
    
    
    for(int i = 0; i<blocks.Length; i+=1)
    {
        
        mat[i] = blocks[i].GetComponent<MeshRenderer>().materials[1];
    }
    
   }
   
  
  void Update() 
  {
      if(playerOnTrigger)
      {
          timer2 = 0f;
          
          if(timer < time)
          {
             
              brightness = Mathf.Lerp(0.6f, 1.5f, timer / time);
              timer += Time.deltaTime;
              for(int i=0; i<blocks.Length; i++)
              {
                mat[i].SetColor("_EmissionColor", new Color(brightness, brightness, brightness));
              }
              
          }
      }
      else
      {
          timer = 0f;
          
          if(timer2 < time)
          {
             
              brightness = Mathf.Lerp(brightness, 0.6f, timer2 / time);
              timer2 += Time.deltaTime;
              for(int i=0; i<blocks.Length; i++)
              {
                mat[i].SetColor("_EmissionColor", new Color(brightness, brightness, brightness));
              }
          }
      }
  }
  
  
   private void OnTriggerEnter(Collider other) 
   {
      
       if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
       {
          playerOnTrigger = true;
       }
   }
   private void OnTriggerExit(Collider other) 
   {
      
      
       if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
       {
           playerOnTrigger = false;
          
       }
   }

   
   
   
}
