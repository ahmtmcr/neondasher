using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTestAccounts : MonoBehaviour
{
    
    [SerializeField] InputField email;
    [SerializeField] InputField password;

    public void Test1()
    {
        email.text = "test1@test.com";
        password.text = "12345678";
    }
    public void Test2()
    {
        email.text = "test2@test.com";
        password.text = "12345678";
    }
}
