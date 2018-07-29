using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Linq;
using Gamedef;
using CsvHelper;

public class SocketLoginAPI : SocketBase
{
    ////用于保存server list
    //public List<LoadLoginConfig> serverAddressList;

    ////读取server list
    //public void GetCSVFile()
    //{
    //    string fileFullName = APP_CONST.CsvFilePath + "LoginServer.csv";
    //    var sr = new StreamReader(fileFullName);
    //    var csv = new CsvReader(sr);
    //    csv.Configuration.TrimFields = true;
    //    csv.Configuration.TrimHeaders = true;
    //    csv.Read();
    //    serverAddressList = csv.GetRecords<LoadLoginConfig>().ToList();//数组缓存 
    //}

    ////设置登录的Server ip
    //public void SetServerAddressById(int _id)
    //{
    //    foreach (LoadLoginConfig lic in serverAddressList)
    //    {
    //        if (lic.id == _id)
    //        {
    //            _serverAddress = lic.ip;
    //            return;
    //        }
    //    }
    //}

    ////初始化登陆Socket
    //public void InitSocketLogin()
    //{
    //    GetCSVFile();

    //    _netWorker.RegisterMessage<LoginACK>(loginAck);     //注册接受登陆的反馈信息，并作登陆消息处理
    //    _netWorker.RegisterMessage<VerifyGameACK>(verifyGameAck); //注册返回登录结果的消息
    //    _netWorker.RegisterMessage<RoleDataACK>(roleDataAck);     //注册返回角色信息的消息
    //    _netWorker.RegisterMessage<EnterGameACK>(enterGameAck);    //注册接受是否正常进入游戏消息


    //    //测试使用，实际根据不同情况选择不同服务器
    //    SetServerAddressById(2);
    //}


    ////登陆接口
    //public bool OnLogin(string _name, string _password)
    //{
    //    _netWorker.Connect(_serverAddress);

    //    while (_netWorker.GetSocket() == null || !_netWorker.IsConnect())
    //    {
    //        if (!WaitTime(_waitTimeOut))
    //        {
    //            _connectTime = 0.0f;
    //            return false;
    //        }
    //    }


    //    LoginREQ loginReq = new LoginREQ();
    //    loginReq.AccountName = _name;
    //    loginReq.Password = _password;
    //    loginReq.ClientVersion = "0.1";

    //    _playerInfor._AccountName = loginReq.AccountName;   //保存账号名称信息

    //    _netWorker.SendMessage(loginReq);

    //    return true;
    //}

    ////确认进入游戏
    //public bool EnterGame()
    //{
    //    if (_netWorker.GetSocket() == null || !_netWorker.IsConnect() || !_isLogin)
    //        return false;

    //    EnterGameREQ enterGame = new EnterGameREQ();

    //    _netWorker.SendMessage(enterGame);
    //    return true;
    //}

    ////创建角色请求
    //public void CreateRole(string roleName, Int32 roleType)
    //{
    //    if (_netWorker.GetSocket() == null || !_netWorker.IsConnect() || !_isLogin)
    //        return;

    //    if (_playerInfor._RoleID != 0)
    //        return;

    //    //发送要创建的角色信息
    //    CreateRoleREQ createRoleData = new CreateRoleREQ();
    //    createRoleData.AccountName = _playerInfor._AccountName;
    //    createRoleData.RoleName = roleName;
    //    createRoleData = roleType;

    //    _netWorker.SendMessage(createRoleData);

    //}



    ////根据消息包定义各自的委托（Action）函数
    //Action<object> loginAck = new Action<object>(LoginAck);
    //Action<object> verifyGameAck = new Action<object>(VerifyGameAck);
    //Action<object> roleDataAck = new Action<object>(RoleDataAck);
    //Action<object> enterGameAck = new Action<object>(EnterGameAck);


    //public static void LoginAck(object data)
    //{
    //    LoginACK loginack = new LoginACK();

    //    loginack.MergeFrom((LoginACK)data);

    //    _netWorker.Stop();            //关闭与LoginServer的连接

    //    if (loginack.Result == "")
    //    {
    //        _netWorker.Connect(loginack.ServerAddr); //连接GameServer


    //        while (_netWorker.GetSocket() == null || !_netWorker.IsConnect())
    //        {
    //            Thread.Sleep((int)(Time.deltaTime * 1000));
    //            _connectTime += Time.deltaTime;
    //            if (_connectTime > _waitTimeOut)
    //            {
    //                _connectTime = 0.0f;
    //                _isLogin = false;
    //                return;
    //            }
    //        }

    //        if (loginack.RoleID == 0)
    //        {
    //            _isCreateRole = true;
    //            return;
    //        }

    //        _isCreateRole = false;

    //        VerifyGameREQ gameReq = new VerifyGameREQ();
    //        gameReq.RoleID = loginack.RoleID;
    //        gameReq.FamilyID = loginack.FamilyID;
    //        gameReq.Token = loginack.Token;

    //        _netWorker.SendMessage(gameReq);

    //        //保存账户角色ID信息
    //        _playerInfor._RoleID = loginack.RoleID;

    //    }
    //    else
    //        _isLogin = false;

    //}

    //public static void VerifyGameAck(object data)
    //{
    //    VerifyGameACK verifyGame = new VerifyGameACK();
    //    verifyGame.MergeFrom((VerifyGameACK)data);

    //    if (verifyGame.Result == VerifyGameResult.VerifyOk)
    //    {
    //        _isLogin = true;

    //        RoleDataREQ roledata = new RoleDataREQ();
    //        roledata.RoleID = _playerInfor._RoleID;

    //        _netWorker.SendMessage(roledata);               //自动发送获取角色信息请求
    //    }
    //}


    //public static void RoleDataAck(object data)
    //{
    //    RoleDataACK roleData = new RoleDataACK();
    //    roleData.MergeFrom((RoleDataACK)data);

    //    //保存角色信息
    //    _playerInfor._RoleName = roleData.RoleName;
    //    _playerInfor._RoleType = roleData.RoleType;
    //    _playerInfor._FamilyID = roleData.FamilyID;
    //}



    //public static void CreateRoleAck(object data)
    //{
    //    CreateRoleACK createRoleAck = new CreateRoleACK();
    //    createRoleAck.MergeFrom((CreateRoleACK)data);

    //    if (createRoleAck.Result == CreateRoleResult.CreateRoleOk)
    //    {
    //        _playerInfor._RoleID = createRoleAck.RoleID;
    //        _playerInfor._RoleName = createRoleAck.RoleName;
    //        _playerInfor._RoleType = createRoleAck.PType;
    //    }
    //}


    //public static void EnterGameAck(object data)
    //{
    //    EnterGameACK enterReult = new EnterGameACK();
    //    enterReult.MergeFrom((EnterGameACK)data);


    //}


    //public void Awake()
    //{
    //    InitSocketLogin();
    //}

}
