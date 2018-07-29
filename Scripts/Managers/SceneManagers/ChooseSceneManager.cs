using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSceneManager : MonoBehaviour {

    void Start()
    {
        
        if (UIManager.Instance.Canvas != null)
        {
            UIManager.Instance.CreateOrShowWindow("ChooseWindow", UIManager.Instance.Canvas.transform);
        }

    }
}
