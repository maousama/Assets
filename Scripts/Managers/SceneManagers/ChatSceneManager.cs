using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSceneManager : MonoBehaviour
{
    void Start()
    {

        if (UIManager.Instance.Canvas != null)
        {
            UIManager.Instance.CreateOrShowWindow("ChatMessageBox", UIManager.Instance.Canvas.transform);
        }

    }
}
