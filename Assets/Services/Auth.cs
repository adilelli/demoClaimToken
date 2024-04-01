using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//update Models.Auth from Authentication
public class Auth : MonoBehaviour
{
    public string auth;
    public string GetAuthFromWebGL()
    {
        int pm = Application.absoluteURL.IndexOf("?"); 
        if (pm != -1)
        {
            auth = Application.absoluteURL.Split("?"[0])[1].Split("=")[1];
            Debug.Log("new user: " + auth);
        }
        return auth;
    }
}
