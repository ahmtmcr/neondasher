using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    
    private float endGameCount = 0.0f;
    private float endGameTime = 5.0f;
    private SoundController soundController;
    public int endGamePlayerCounter = 0;
    
    GameStateManager gameStateManager;
    
    void Start() 
    {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }
   
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            endGamePlayerCounter += 1;
        }
        if(other.gameObject.tag == "Player2")
        {
            endGamePlayerCounter += 1;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2" )
        {
           endGamePlayerCounter -= 1;
        }
    }
    
  

    
    void Update()
    {
        if(endGamePlayerCounter == 2)
        {
            endGameCount += Time.deltaTime;
            if(endGameCount > endGameTime){
                endGameCount = 0.0f;
                soundController.PlayLevelEndedAudio();
                gameStateManager.LeaveGame("Match Ended (Game Finished)");
             }
        }
    }
}
