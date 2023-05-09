using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] GameObject GameTittle;
    
    [SerializeField] public GameObject LoginAndRegisterWithEmailUI;
    [SerializeField] public GameObject FindMatchUI;
    [SerializeField] public GameObject LoginWithDeviceIDUI;
    [SerializeField] public GameObject LoginUI;
    [SerializeField] public GameObject LoginWithEmailUI;



    [SerializeField] public GameObject FindMatchButton;
    [SerializeField] public GameObject CancelMatchButton;


    [SerializeField] public GameObject escMenu;
    [SerializeField] public GameObject backButton;
    [SerializeField] public GameObject LogoutButton;

    [SerializeField] public GameObject ErrorMessages;


    [SerializeField] public GameObject optionsMenu;
    


  

    
    public void FindAndCancelMathUI()
    {
        if (FindMatchButton.activeSelf)
        {
            FindMatchButton.SetActive(false);
            LogoutButton.SetActive(false);
            CancelMatchButton.SetActive(true);
        }
        else
        {
            FindMatchButton.SetActive(true);
            LogoutButton.SetActive(true);
            CancelMatchButton.SetActive(false);
        }
    }

    public void ReturningToFindMatch()
    {
        FindMatchButton.SetActive(true);
        LogoutButton.SetActive(true);
        CancelMatchButton.SetActive(false);
    }

    public void DisableEmailLoginAndRegister()
    {
        LoginAndRegisterWithEmailUI.SetActive(false);
    }
    public void DisableDeviceLogin()
    {
        LoginWithDeviceIDUI.SetActive(false);
    }
    public void EnableFindMatch()
    {
        FindMatchUI.SetActive(true);
    }

    public void DisableLoginAndFindMatchUI()
    {
        LoginUI.SetActive(false);
    }

    public void DisableEmailLogin()
    {
        LoginWithEmailUI.SetActive(false);
        backButton.SetActive(false);
    }
    public void EnableEmailLoginAndDisableRegister()
    {
        LoginAndRegisterWithEmailUI.SetActive(false);
        EnableMailLogin();
        
    }
    public void EnableMailLogin()
    {
        LoginWithEmailUI.SetActive(true);
        backButton.SetActive(true);
    }
    public void DisableFindMatch()
    {
        FindMatchUI.SetActive(false);
    }

    public void EscMenu()
    {   
        if(escMenu.activeSelf == true)
        {
            escMenu.SetActive(false);
        }
        else
        {
            escMenu.SetActive(true);
        }
       
        
    }
    public void CloseEscMenu()
    {
       escMenu.SetActive(false);
    }

    public void GoBack()
    {
        if(LoginWithEmailUI.activeSelf == true)
        {
            LoginWithEmailUI.SetActive(false);
            LoginAndRegisterWithEmailUI.SetActive(true);
            backButton.SetActive(false);
        }
    }

    public void LogOut()
    {
        DisableFindMatch();
        EnableMailLogin();
    }

    public void CloseErrorMesseages()
    {
        ErrorMessages.SetActive(false);
    }

    public void EnableOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }
    public void DisableOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }
    public void DisableGameTittle()
    {
        GameTittle.SetActive(false);
    }
    public void EnableGameTittle()
    {
        GameTittle.SetActive(true);
    }

    

    
}
