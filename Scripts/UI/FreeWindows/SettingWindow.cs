using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingWindow : FreeWindow
{
    public override string Name
    {
        get
        {
            return "SettingWindow";
        }
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            switch (buttonsList[i].name)
            {
                case "Btn_Back":
                    buttonsList[i].onClick.AddListener(Btn_BackClick);
                    break;
                case "Btn_Save":
                    buttonsList[i].onClick.AddListener(Btn_SaveClick);
                    break;
            }
        }
    }

    private void Btn_SaveClick()
    {
        UIManager.Instance.HideWindow(Name);
    }

    private void Btn_BackClick()
    {
        UIManager.Instance.HideWindow(Name);
    }
}
