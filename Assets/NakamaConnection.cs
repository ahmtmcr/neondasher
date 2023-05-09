using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Nakama;

public class NakamaConnection : MonoBehaviour
{
    
   
    
    
    
    
    [SerializeField] GameStateManager GameStateManager;
    [SerializeField] UIManager UIManager;
   
   
   
    //private bool load_map = false; //change when export
    
  
    [SerializeField] Text _username;
    [SerializeField] InputField DeviceLoginUsername;
    [SerializeField] InputField EmailRegisterAndLogin;
    [SerializeField] InputField EmailRegisterAndLoginPassword;
    [SerializeField] InputField EmailRegisterAndLoginUsername;
    [SerializeField] InputField EmailLogin;
    [SerializeField] InputField EmailLoginPassword;
    
    

    
    
    
    
    private string scheme = "http";
    private string host = "20.68.171.185";
    private int port = 7350;
    private string serverKey = "defaultkey";

    public IClient client;
    public ISession session;
    public ISocket socket;

    private string ticket;
    

 

    void Start()
    {
       
    }
   
    public async void TestConnection()
    {
        client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);
        
        try
        {
             session = await client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier);
             UIManager.ErrorMessages.SetActive(true);
             UIManager.ErrorMessages.GetComponentInChildren<Text>().text = "Succesfully connected";
        }
        catch(ApiResponseException ex)
        {
            UIManager.ErrorMessages.SetActive(true);
            UIManager.ErrorMessages.GetComponentInChildren<Text>().text = ex.Message;
        }
    }
    public async void AuthenticateWithEmailAndRegister()
    {
        client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);
        try
        {
            session = await client.AuthenticateEmailAsync(EmailRegisterAndLogin.text, EmailRegisterAndLoginPassword.text, EmailRegisterAndLoginUsername.text, true);
        }
        catch(ApiResponseException ex)
        {
            UIManager.ErrorMessages.SetActive(true);
            UIManager.ErrorMessages.GetComponentInChildren<Text>().text = ex.Message;
        }
        
        
        
        
        
        
        socket = client.NewSocket();
        await socket.ConnectAsync(session, true);

        
        
        
        
        _username.text = session.Username;

        UIManager.DisableEmailLoginAndRegister();
        UIManager.EnableFindMatch();

        GameStateManager.setupCallbacks = true;

        

    }
    public async void AuthenticateWithEmail()
    {
        client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);
        try
        {
            session = await client.AuthenticateEmailAsync(EmailLogin.text, EmailLoginPassword.text, null, false);
        }
        catch(ApiResponseException ex)
        {
            UIManager.ErrorMessages.SetActive(true);
            UIManager.ErrorMessages.GetComponentInChildren<Text>().text = ex.Message;
        }
        
        
        
        
        
        socket = client.NewSocket();
        await socket.ConnectAsync(session, true);

        
        
        
        
        _username.text = session.Username;

        UIManager.DisableEmailLogin();
        UIManager.EnableFindMatch();

        GameStateManager.setupCallbacks = true;
        
    }

    public async void AuthenticateWithDeviceID()
    {
        client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);
        session = await client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier, DeviceLoginUsername.text, true);
        socket = client.NewSocket();
        await socket.ConnectAsync(session, true);

        
        
        
        _username.text = session.Username;

        UIManager.DisableDeviceLogin();
        UIManager.EnableFindMatch();

        GameStateManager.setupCallbacks = true;
        
        
    }

    public async void FindMatch()
    {
        Debug.Log("Finding match...");
        var matchmakingTicket = await socket.AddMatchmakerAsync("*", 2, 2);
        ticket = matchmakingTicket.Ticket;
        UIManager.FindAndCancelMathUI();
    }

    public async void CancelMatchmaking()
    {
        Debug.Log("Canceling match...");
        await socket.RemoveMatchmakerAsync(ticket);
        UIManager.FindAndCancelMathUI();
        

    }

  
   public async void LogOut()
   {
      await client.SessionLogoutAsync(session);
      Debug.Log(session);
   }

    // private async void LoadMap()
    // {
    //     await socket.SendMatchStateAsync(matchId, 1, "", null);//change when export
    // }

    // private void OnReceivedMatchState(IMatchState matchState)
    // {
       
    //     if(matchState.OpCode == 1)
    //     {
            
    //         load_map = true;//change when export
    //     }
        
    // }

 
    // void Update()
    // {
    //     if(load_map)
    //     {
    //         SceneManager.LoadScene("SampleScene");//change when export
    //         load_map = false;//change when export
    //     }

     
    // }
    

   


}
