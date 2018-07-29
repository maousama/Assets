using UnityEngine;
using System;
using System.Threading;
using Gamedef;
public class SocketBase : MonoBehaviour
{

    public static NetworkPeer _netWorker = new NetworkPeer();
    public string _serverAddress = SocketConnection.ServerAddress;   //LoginServer IP
    public static bool _isLogin = false;                  //用户是否正常登陆 
    public static bool _isCreateRole = false;             //用户是否需要创建角色


    //超时时长
    public static float _connectTime = 0.0f;              //连接服务器所消耗的时间
    public static float _waitTimeOut = 5.0f;             //连接等待的超时时长

    //发送心跳包间隔时间
    protected float _sendHeartBeatTime = 30.0f; //秒


    public SocketBase()
    {
        _netWorker.RegisterMessage<HeartBeatACK>(heartbeatAck);   //接收心跳
        //_netWorker.RegisterMessage<ShowErrorMsg>(showErrorMsg);  
    }

    Action<object> heartbeatAck = new Action<object>(ReceviceHeartPacket);


    private static float _elaspedTime = 0.0f;
    //发心跳包
    public void SendHeartPacket()
    {
        if (_netWorker.GetSocket() != null && _netWorker.IsConnect() &&
            Time.time - _elaspedTime > _sendHeartBeatTime)
        {
            HeartBeatREQ heartbeat = new HeartBeatREQ();
            _netWorker.SendMessage(heartbeat, false);
            _elaspedTime = Time.time;
        }

    }

    //接收心跳包，并对连接做相应处理
    public static void ReceviceHeartPacket(object data)
    {
        HeartBeatACK heartbeat = new HeartBeatACK();
        heartbeat.MergeFrom((HeartBeatACK)data);
    }

    //连接服务器的等待时间
    public bool WaitTime(float _time)
    {
        Thread.Sleep((int)(Time.deltaTime * 1000));
        _connectTime += Time.deltaTime;
        if (_connectTime > _time)
            return false;

        return true;
    }



    #region Unity接口
    private void Awake()
    {
        OnAwake();
    }
    /// <summary>
    /// 在Awake中调用的函数
    /// </summary>
    protected virtual void OnAwake()
    {
        
    }

    private void Start()
    {
        OnStart();
    }
    /// <summary>
    /// 在Start中调用的函数
    /// </summary>
    protected virtual void OnStart()
    {
        
    }

    private void Update()
    {
        OnUpdate();
    }
    /// <summary>
    /// Update中调用函数
    /// </summary>
    protected virtual void OnUpdate()
    {
        SendHeartPacket();
        _netWorker.Polling();
    }



    #endregion

}
