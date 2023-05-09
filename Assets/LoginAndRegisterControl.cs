using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginAndRegisterControl : MonoBehaviour
{
    
    
    // [SerializeField] Text loginControlErrors;
    [SerializeField] Button loginAndRegisterButton;
    [SerializeField] Button loginButton;
    
    [SerializeField] InputField email;
    [SerializeField] Text emailErrorText;
    
    [SerializeField] InputField password;
    [SerializeField] Text passwordErrorText;
    
    [SerializeField] InputField confirmPassword;
    [SerializeField] Text confirmPasswordErrorText;
    
    [SerializeField] InputField username;
    [SerializeField] Text usernameErrorText;


    private string requiredText = "This Field is required.";
    private string emptyString = "";

    [SerializeField] private bool isThisRegister = true;


    
    void Start()
    {
       if(isThisRegister)
       {
            loginAndRegisterButton.interactable = false;
            emailErrorText.text = "";
            passwordErrorText.text = "";
            confirmPasswordErrorText.text = "";
            usernameErrorText.text = "";
       }
       else
       {
            loginButton.interactable = false;
            emailErrorText.text = "";
            passwordErrorText.text = "";
       }
    }

    
    void Update()
    {
       if(isThisRegister)
       {
            CheckIfEmailIsEntered();
            CheckIfPasswordIsEntered();
            CheckIfConfirmPasswordIsEntered();
            CheckIfPasswordsAreMatching();
            CheckIfUsernameIsEntered();
            EnableLoginAndRegisterButton();
       }
       else
       {
            CheckIfEmailIsEntered();
            CheckIfPasswordIsEntered();
            EnableLoginButton();
       }

    }

    private void CheckIfEmailIsEntered()
    {
        if(email.text != "")
        {
            emailErrorText.text = emptyString;
        }
        else
        {
            emailErrorText.text = requiredText;
        }
    }
    private void CheckIfPasswordIsEntered()
    {
        if(password.text != "")
        {
            passwordErrorText.text = emptyString;
        }
        else
        {
            passwordErrorText.text = requiredText;
        }
    }
    private void CheckIfConfirmPasswordIsEntered()
    {
        if(confirmPassword.text != "")
        {
            confirmPasswordErrorText.text = emptyString;
        }
        else
        {
            confirmPasswordErrorText.text = requiredText;
        }
    }
    private void CheckIfPasswordsAreMatching()
    {
        if(password.text == confirmPassword.text){}
        else
        {
            confirmPasswordErrorText.text = "Passwords are not matching";
            passwordErrorText.text = "Passwords are not matching";
        }
    }
    private void CheckIfUsernameIsEntered()
    {
        if(username.text != "")
        {
            usernameErrorText.text = emptyString;
        }
        else
        {
            usernameErrorText.text = requiredText;
        }
    }
    private void EnableLoginAndRegisterButton()
    {
        if(isThisRegister)
        {
            if(email.text != "" && password.text != "" && confirmPassword.text != "" && username.text != "" && password.text == confirmPassword.text)
            {
                loginAndRegisterButton.interactable = true;
            }
            else
            {
                loginAndRegisterButton.interactable = false;
            }
        }
       
        
    }
    private void EnableLoginButton()
    {
        if(!isThisRegister)
        {
            if(email.text != "" && password.text != "")
            {
                loginButton.interactable = true;
            }
        }
    }
   

}
