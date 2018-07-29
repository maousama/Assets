using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;

public class ChatMessageBox : MessageBox
{

    public override string Name
    {
        get
        {
            return "ChatMessageBox";
        }
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            switch (buttonsList[i].name)
            {

                case "Btn_Conn":
                    buttonsList[i].onClick.AddListener(ConnBtnClick);
                    break;
            }
        }
        

    }
    private void ConnBtnClick()
    {
        ChatNetController.Instance.ConnBtnClick(inputFieldDic["InputField_Conn"].text);
        UIManager.Instance.CloseWindow("ChatMessageBox");
        UIManager.Instance.CreateOrShowWindow("ChatWindow",UIManager.Instance.Canvas);
    }


}
