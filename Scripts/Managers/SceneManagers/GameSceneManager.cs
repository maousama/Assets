using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour{


    void Start()
    {

        if (UIManager.Instance.Canvas != null)
        {
            UIManager.Instance.CreateOrShowWindow("CombatWindow", UIManager.Instance.Canvas.transform);
        }

    }
}
