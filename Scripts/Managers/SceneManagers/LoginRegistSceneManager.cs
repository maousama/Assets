using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRegistSceneManager : MonoBehaviour {

    void Start()
    {
        WindowBase bg = UIManager.Instance.CreateOrShowWindow("LoginRegistBackGroundWindow", UIManager.Instance.Canvas);
        if (bg != null)
        {
            UIManager.Instance.CreateOrShowWindow("LoginRegistWindow", bg.transform);
        }
        
    }
}
