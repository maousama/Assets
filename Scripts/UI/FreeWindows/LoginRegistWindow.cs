using UnityEngine;
using System.Collections;
using Gamedef;

public class LoginRegistWindow : FreeWindow
{
    public override string Name
    {
        get
        {
            return "LoginRegistWindow";
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            switch (buttonsList[i].name)
            {
                case "Btn_Regist":
                    buttonsList[i].onClick.AddListener(RegistBtnClick);
                    break;
                case "Btn_Login":
                    buttonsList[i].onClick.AddListener(LoginBtnClick);
                    break;
            }
        }
    }

    /// <summary>
    /// 注册按钮连接方法
    /// </summary>
    /// <param name="o"></param>
    private void RegistBtnClick()
    {
        
        RegistLogin registLogin = GameObject.Find("NetManager").GetComponent<RegistLogin>();
        WindowBase msgBox = UIManager.Instance.CreateOrShowWindow("LoginRegistMessageBox", transform);
        registLogin.msgBox = msgBox;
        if (registLogin != null)
        {
            if (SocketBase._isLogin)
            {
                return;
            }
            SocketBase._netWorker.Stop();
            SocketBase._netWorker.Connect(registLogin._serverAddress);
            while (!SocketBase._netWorker.IsConnect() || SocketBase._netWorker.GetSocket() == null)
            {
                if (!registLogin.WaitTime(SocketBase._waitTimeOut))
                {
                    //Debug.Log("连接超时");
                    msgBox.buttonsList[0].gameObject.SetActive(true);
                    msgBox.textDic["Text_Tip"].text = "连接超时";
                    return;
                }
            }
            RegisterAccountReq req = new RegisterAccountReq();
            req.AccountName = inputFieldDic["InputField_Username"].text;
            req.Password = inputFieldDic["InputField_Password"].text;
            registLogin.accountName = inputFieldDic["InputField_Username"].text;
            SocketBase._netWorker.SendMessage(req);
        }
    }
    /// <summary>
    /// 登录按钮click方法
    /// </summary>
   private  void LoginBtnClick()
    {
        RegistLogin registLogin = GameObject.Find("NetManager").GetComponent<RegistLogin>();
        WindowBase msgBox = UIManager.Instance.CreateOrShowWindow("LoginRegistMessageBox", transform);
        registLogin.msgBox = msgBox;

        if (SocketBase._isLogin)
        {
            return;
        }
        SocketBase._netWorker.Stop();
        SocketBase._netWorker.Connect(registLogin._serverAddress);
        while (!SocketBase._netWorker.IsConnect() || SocketBase._netWorker.GetSocket() == null)
        {
            if (!registLogin.WaitTime(SocketBase._waitTimeOut))
            {
                //Debug.Log("连接超时");
                msgBox.buttonsList[0].gameObject.SetActive(true);
                msgBox.textDic["Text_Tip"].text = "连接超时";
                return;
            }
        }
        LoginREQ req = new LoginREQ();
        req.AccountName = inputFieldDic["InputField_Username"].text;
        req.Password = inputFieldDic["InputField_Password"].text;
        SocketBase._netWorker.SendMessage(req);
    }
}
