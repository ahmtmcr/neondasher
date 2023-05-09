using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;

public class NetworkPlayerSendInfo : MonoBehaviour
{
    
    
    
    
    
    
    public float StateFrequency = 0.05f;
    private float stateSyncTimer;
    
    
    private GameStateManager gameStateManager;

    
    

    
    
    
    
    void Start()
    {
       
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        
        
    }

    void Update() 
    {
        // gameStateManager.SendMatchState(
        //         3,
        //         MatchDataJson.TeleportPlus(teleportCount.playerCounter));
      
    }
  
   
    void LateUpdate()
    {
        if(stateSyncTimer <= 0)
        {
            gameStateManager.SendMatchState(
                1, 
                MatchDataJson.Position3D(transform.position));

            gameStateManager.SendMatchState(
                2,
                MatchDataJson.Rotation3D(transform.eulerAngles));
         
            
        stateSyncTimer = StateFrequency;
        }

        stateSyncTimer -= Time.deltaTime;
    }
   
    
}
