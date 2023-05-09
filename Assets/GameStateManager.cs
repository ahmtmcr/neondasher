using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nakama;
using System.Text;
using Nakama.TinyJson;



public class GameStateManager : MonoBehaviour
{
     
    [SerializeField] EndGame endgame;
    public bool playerSpawned = false;
    // public int leavedGame = 0;

    private Transform[] spawnPoints;
    public GameObject SpawnPoints;
    
    public int a = 0;
    public GameObject cam;
    
    public bool setupCallbacks = false;
    private IDictionary<string, GameObject> players;
    
    
    
    [SerializeField] GameObject LocalPlayer;
    [SerializeField] GameObject NetworkPlayer;
    [SerializeField] GameObject Level;
    [SerializeField] public NakamaConnection nakamaConnection;
    [SerializeField] UIManager uIManager;

    

    

    private IUserPresence localUser;

    public IMatch currentMatch;


    private UnityMainThreadDispatcher mainThreadDispatcher;

    void Start() 
    {
        mainThreadDispatcher = UnityMainThreadDispatcher.Instance();
        players = new Dictionary<string, GameObject>();
    }
   void Update() 
   {
       EscMenu();
    //    if(leavedGame == 1)
    //     {
    //         SendMatchState(4, MatchDataJson.LeavedGame(leavedGame));
    //         LeaveGame();
    //         leavedGame = 0;
    //     }
       if (setupCallbacks)
       {
           nakamaConnection.socket.ReceivedMatchmakerMatched += m => mainThreadDispatcher.Enqueue(() => OnReceivedMatchmakerMatched(m));
           nakamaConnection.socket.ReceivedMatchPresence += m => mainThreadDispatcher.Enqueue(() => OnReceivedMatchPresence(m));
        //    nakamaConnection.socket.ReceivedMatchState += m => mainThreadDispatcher.Enqueue(() => OnReceivedMatchState(m));
           setupCallbacks = false;
       }


        
   }
   
    // private void OnReceivedMatchState(IMatchState matchState)
    // {
    //     switch (matchState.OpCode)
    //     {
    //         // case 1:
    //         //     UpdatePosition3D(matchState.State);
    //         //     break;
    //         // case 2:
    //         //     UpdateDirection3D(matchState.State);
    //         //     break;
    //         // case 3:
    //         //     UpdateTeleportCount(matchState.State);
    //         //     break;
    //         // case 4:
    //         //     AfterLeavedGame(matchState.State);
    //         //     break;

    
    //     }
    // }
   
    
    public void GameStarted()
    {
        Level.SetActive(true);
        cam.SetActive(false);
    }
    private void OnReceivedMatchPresence(IMatchPresenceEvent matchPresenceEvent)
    {
        foreach (var user in matchPresenceEvent.Joins)
        {
            SpawnPlayer(matchPresenceEvent.MatchId, user);
        }
        foreach (var user in matchPresenceEvent.Leaves)
        {
            if (players.ContainsKey(user.SessionId))
            {
                Destroy(players[user.SessionId]);
                players.Remove(user.SessionId);
                LeaveGame("Match Ended (Player Left)");
                
            }
        }
    }
    private async void OnReceivedMatchmakerMatched(IMatchmakerMatched matchmakerMatched)
    {
        
        
       
        localUser = matchmakerMatched.Self.Presence;
        var match = await nakamaConnection.socket.JoinMatchAsync(matchmakerMatched);
        currentMatch = match;
        
        
        uIManager.DisableFindMatch();
        uIManager.DisableGameTittle();
        GameStarted();

        
        foreach (var user in match.Presences)
        {
            SpawnPlayer(match.Id, user);
        }

        
       
        

    }

    private void SpawnPlayer(string matchId, IUserPresence user, int spawnIndex = -1)
    {
        
        
        if (players.ContainsKey(user.SessionId))
        {
            return;
        }
        
        var isLocal = user.SessionId == localUser.SessionId;
        
        
        // Vector2 SpawnPoint = new Vector2(-4,5);2D
        // Vector2 SpawnPoint2 = new Vector2(-5,5);
        // Vector2[] spawnPoint = {SpawnPoint, SpawnPoint2};

        // Vector3 SpawnPoint = new Vector3(-2, 6, 0);
        // Vector3 SpawnPoint2 = new Vector3(2, 6, 0);
        // Vector3[] spawnPoint = {SpawnPoint, SpawnPoint2};

        var spawnPoint = spawnIndex == -1 ?
            SpawnPoints.transform.GetChild(Random.Range(0, SpawnPoints.transform.childCount - 1)) :
            SpawnPoints.transform.GetChild(spawnIndex);
        
       
        
        var playerPrefab = isLocal ? LocalPlayer : NetworkPlayer;
        
        
        var player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        a += 1;
        players.Add(user.SessionId, player);
        
        playerSpawned = true;

       
        
    }

    public void SendMatchState(int opCode, string state)
    {
        nakamaConnection.socket.SendMatchStateAsync(currentMatch.Id, opCode, state);
    }

    public void EscMenu()
    {
        if(playerSpawned)
        {
            uIManager.EnableOptionsMenu();
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                uIManager.EscMenu();
            }
        }
        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     uIManager.OpenEscMenu();
        // }

    }

    public void ContinueGame()
    {
        uIManager.CloseEscMenu();
    }

    public async void LeaveGame(string reason)
    { 
        await nakamaConnection.socket.LeaveMatchAsync(currentMatch);
        currentMatch = null;
        localUser = null;
        foreach (var player in players.Values)
        {
            Destroy(player);
        }

         players.Clear();
         a = 0;
         Level.SetActive(false);
         cam.SetActive(true);
         uIManager.ReturningToFindMatch();
         uIManager.escMenu.SetActive(false);
         uIManager.EnableFindMatch();
         uIManager.DisableOptionsMenu();
         uIManager.ErrorMessages.SetActive(true);
         uIManager.EnableGameTittle();
         uIManager.ErrorMessages.GetComponentInChildren<Text>().text = reason;

         playerSpawned = false; //closeescmenu
         endgame.endGamePlayerCounter = 0;
        //  leavedGame = 1;

        //  SendMatchState(4, MatchDataJson.LeavedGame(leavedGame));
        
         
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    // private void AfterLeavedGame(byte[] state)
    // {
        
    //     var stateDictionary = GetStateAsDictionary(state);

    //     var leavedGameBool = int.Parse(stateDictionary["leavedGame"]);
        
    //     leavedGameBool = leavedGame;
        
        
    // }


    // private IDictionary<string, string> GetStateAsDictionary(byte[] state)
    // {
    //     return Encoding.UTF8.GetString(state).FromJson<Dictionary<string, string>>();
    // }    





}
