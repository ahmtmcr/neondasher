using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Nakama.TinyJson;
using System.Text;

public class PlayerReceiveSound : MonoBehaviour
{
    
    private SoundController soundController;
    private GameStateManager gameStateManager;
    
    
    void Start()
    {
        soundController = soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        if(gameStateManager.playerSpawned == true)
        {
            gameStateManager.nakamaConnection.socket.ReceivedMatchState += EnqueueOnReceivedMatchState;
        }
        
    }
    private void EnqueueOnReceivedMatchState(IMatchState matchState)
    {
        var unityMainThreadDispatcher = UnityMainThreadDispatcher.Instance();
        unityMainThreadDispatcher.Enqueue(() => OnReceivedMatchState(matchState));
    }
    private void OnReceivedMatchState(IMatchState matchState)
    {
    
            switch (matchState.OpCode)
            {
       
                case 4:
                    PlayNetworkAudio(matchState.State);
                    break;
            }
        
        
    }
    private IDictionary<string, string> GetStateAsDictionary(byte[] state)
    {
        return Encoding.UTF8.GetString(state).FromJson<Dictionary<string, string>>();
    }    
    private void PlayNetworkAudio(byte[] state)
    {
        var stateDictionary = GetStateAsDictionary(state);

        var sound = (stateDictionary["sound"]);
        

        Debug.Log(sound);
       
    }

}
