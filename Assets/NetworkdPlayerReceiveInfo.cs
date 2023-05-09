using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Nakama.TinyJson;
using System.Text;

public class NetworkdPlayerReceiveInfo : MonoBehaviour
{
    
    private GameStateManager gameStateManager;
   
   
   
    private float lerpTimer;
  
    private Vector3 lerpFromPosition;
    private Vector3 lerpToPosition;
    
    private bool lerpPosition;
    public float LerpTime = 0.05f;
    
    
    void Start()
    {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
      
        
        if(gameStateManager.playerSpawned == true)
        {
            gameStateManager.nakamaConnection.socket.ReceivedMatchState += EnqueueOnReceivedMatchState;
        }
        
        

       
    }

    void LateUpdate() 
    {
 
        if(!lerpPosition)
        {
            return;
        }

        transform.position = Vector3.Lerp(lerpFromPosition, lerpToPosition, lerpTimer / LerpTime);
        lerpTimer += Time.deltaTime;

        if(lerpTimer >= LerpTime)
        {
            transform.position = lerpToPosition;
            lerpPosition = false;
        }

       
    }

   
    private void EnqueueOnReceivedMatchState(IMatchState matchState)
    {
         if (this != null){
             var unityMainThreadDispatcher = UnityMainThreadDispatcher.Instance();
             unityMainThreadDispatcher.Enqueue(() => OnReceivedMatchState(matchState));
        }
       
    }
    private void OnReceivedMatchState(IMatchState matchState)
    {
        switch (matchState.OpCode)
        {
            case 1:
                UpdatePosition3D(matchState.State);
                break;
            case 2:
                UpdateDirection3D(matchState.State);
                break;
            // case 3:
            //     UpdateTeleportCount(matchState.State);
            //     break;
            // case 4:
            //     UpdateTeleportCount(matchState.State);
            //     break;

    
        }
    }

    private IDictionary<string, string> GetStateAsDictionary(byte[] state)
    {
        return Encoding.UTF8.GetString(state).FromJson<Dictionary<string, string>>();
    }    


    private void UpdatePosition3D(byte[] state)
    {
        var stateDictionary = GetStateAsDictionary(state);

        var position = new Vector3(
            float.Parse(stateDictionary["position.x"]),
            float.Parse(stateDictionary["position.y"]),
            float.Parse(stateDictionary["position.z"])
            );

        //playerTransform.position = position;

        lerpFromPosition = transform.position;
        lerpToPosition = position;
        lerpTimer = 0;
        lerpPosition = true;
    }

    private void UpdateDirection3D(byte[] state)
    {
        var stateDictionary = GetStateAsDictionary(state);
        
        var rotation = new Vector3(
            float.Parse(stateDictionary["rotation.x"]),
            float.Parse(stateDictionary["rotation.y"]),
            float.Parse(stateDictionary["rotation.z"])
            );
            
            
        

        transform.eulerAngles = rotation;
            
    }

    
    
    
    // private void UpdateTeleportCount(byte[] state)
    // {
    //     var stateDictionary = GetStateAsDictionary(state);

    //     var count = float.Parse(stateDictionary["count"]);

    //     teleportCount.slider.value = count;
       

    // }

  


}
