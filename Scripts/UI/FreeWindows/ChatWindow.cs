using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;

public class ChatWindow : FreeWindow {

    public override string Name
    {
        get
        {
            return "ChatWindow";
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            switch (buttonsList[i].name)
            {
              
                case "Btn_Send":
                    buttonsList[i].onClick.AddListener(SendBtnClick);
                    break;
            }
        }
    }

  
   
    private void SendBtnClick()
    {
        ChatNetController.Instance.SendBtnClick(inputFieldDic["InputField_Send"].text);
    }


    private void Update()
    {
        textDic["Text_ShowMessage"].text = ChatNetController.Instance.reveStr;
    }
}
