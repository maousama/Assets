using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWindow : FixedWindow
{
    public override string Name
    {
        get
        {
            return "MainMenuWindow";
        }
    }
    protected override void OnAwake()
    {
        base.OnAwake();

        for (int i = 0; i < buttonsList.Count; i++)
        {
            switch (buttonsList[i].name)
            {
                case "Btn_StartGame":
                    buttonsList[i].onClick.AddListener(Btn_StartGameClick);
                    break;
                case "Btn_Shop":
                    buttonsList[i].onClick.AddListener(Btn_ShopClick);
                    break;
                case "Btn_Quit":
                    buttonsList[i].onClick.AddListener(Btn_QuitClick);
                    break;
                case "Btn_Setting":
                    buttonsList[i].onClick.AddListener(Btn_SettingClick);
                    break;
            }
        }
    }



    private enum DivideType
    {
        Horizontal,
        Vertical,
    }


    private void Btn_SettingClick()
    {
        UIManager.Instance.CreateOrShowWindow(WindowName.SettingWindow, UIManager.Instance.Canvas);
    }

    private void Btn_ShopClick()
    {

    }

    private void Btn_StartGameClick()
    {

    }

    private void Btn_QuitClick()
    {
        Application.Quit();
    }

}
