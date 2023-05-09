using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Nakama.TinyJson;
using System.Text;

public class ReceiveTeleportInfo : MonoBehaviour
{
    public TeleportBars teleportBars;
    
    private GameStateManager gameStateManager;
    
    void Start()
    {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();

        gameStateManager.nakamaConnection.socket.ReceivedMatchState += EnqueueOnReceivedMatchState;
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
       
            case 3:
                UpdateTeleportCount(matchState.State);
                break;

    
        }
    }

    private void UpdateTeleportCount(byte[] state)
    {
        var stateDictionary = GetStateAsDictionary(state);

        var count = float.Parse(stateDictionary["count"]);

        teleportBars.slider.value = count;
       

    }

    private IDictionary<string, string> GetStateAsDictionary(byte[] state)
    {
        return Encoding.UTF8.GetString(state).FromJson<Dictionary<string, string>>();
    }    
}
