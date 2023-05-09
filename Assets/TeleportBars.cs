using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportBars : MonoBehaviour
{
    private bool getPlayer2 = false;

    public Player3DRigidbodyController player;
    
    private GameStateManager gameStateManager;
    
    public Slider slider;

    public Image fill;

    public bool canPress = true;
    
    
 
    private Transform Player1;
    private Transform Player2;





    private float time = 2f;
    private float timer = 0f;
    
    private SoundController soundController;

    
    void Start() 
    {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        Player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
      
      
        
        
        slider.maxValue = 10;
        slider.value = 0;
    }
    
    
    public void TeleportPressed()
    {
        slider.value += 5;
    }

    void Update() 
    {
        if(!getPlayer2)
        {
            if(gameStateManager.a == 2)
            {
                Player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Transform>();
                getPlayer2 = true;
            }
        }

      
        
        if(slider.value == slider.maxValue && canPress)
            {
                canPress = false;
                
                
                Vector3 a = Player1.position;
                Vector3 b = Player2.position;
                Player1.position = b;
                Player2.position = a;

                soundController.PlayTeleportAudio();
            
            }
            if(timer < time && !canPress)
            {
                slider.value -= timer / 60;
                timer += Time.deltaTime;
            }
            else if(timer > time)
            {
                slider.value = 0f;
                canPress = true;
                player.teleportPressed = false;
                timer = 0f;
            }
    }
        
       
   
        
            
           
       
        

       

        
    


  
}
