using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamedef;
using System;
using UnityEngine.UI;

public class RegistLogin : SocketBase
{
    public string accountName = "";
    private Button registBtn;
    private Button loginBtn;
    private Action<object> registAck;
    private Action<object> loginAck;

    public WindowBase msgBox;



    protected override void OnStart()
    {
        base.OnStart();
        registAck += RegistBtnClickAck;
        loginAck += LoginBtnClickAck;
        _netWorker.RegisterMessage<LoginACK>(loginAck);
        _netWorker.RegisterMessage<RegisterAccountAck>(registAck);
    }


    public void RegistBtnClickAck(object o)
    {
        RegisterAccountAck ack = new RegisterAccountAck();
        ack.MergeFrom((RegisterAccountAck)o);
        if (ack.Result == "OK")
        {
            Debug.Log("注册成功");
            Debug.Log(ack.RoleID);
            msgBox.textDic["Text_Tip"].text = "注册成功";
        }
        else
        {
            Debug.Log("注册失败" + ack.Result);
            msgBox.textDic["Text_Tip"].text = "注册失败";
        }

    }
    public void LoginBtnClickAck(object o)
    {
        LoginACK ack = new LoginACK();
        ack.MergeFrom((LoginACK)o);
        if (ack.Result == "OK")
        {
            Debug.Log("登录成功");
            msgBox.textDic["Text_Tip"].text = "登录成功";
            _isLogin = true;
            NetController.netCtrlerInstance.AccountName = accountName;
            NetController.netCtrlerInstance.RoleId = (int)ack.RoleID;
            NetController.netCtrlerInstance.GoldNumber = (int)ack.GoldNumber;
            Debug.Log(ack.RoleID);
        }
        else
        {
            Debug.Log("登录失败" + ack.Result);
            msgBox.textDic["Text_Tip"].text = "登录失败";
        }
    }
    private void OnDestroy()
    {
        _netWorker.UnRegisterMessage<RegisterAccountAck>(registAck);
        _netWorker.UnRegisterMessage<LoginACK>(loginAck);
    }

}
