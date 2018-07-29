using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneManager : ISceneManager
{
    public override void InitUI()
    {
        //Canvas canvas = UIManager.Instance.Canvas.GetComponent<Canvas>();
        //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        //canvas.worldCamera = Camera.main;
        UIManager.Instance.CreateOrShowWindow(WindowName.MainMenuWindow, UIManager.Instance.Canvas);
    }
}
