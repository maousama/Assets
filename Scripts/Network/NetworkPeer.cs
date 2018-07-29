using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Gamedef;
using Google.Protobuf;

public class NetworkPeer : NetworkPeerBase
{

    MessageDispatcher _dispatcher = new MessageDispatcher();

    protected MessageMetaSet _metaSet;

    public NetworkPeer()
    {
        _metaSet = PeerManager.Instance.MsgMeta;

        MsgID_Connected = _metaSet.GetByType<PeerConnected>().id;
        MsgID_Disconnected = _metaSet.GetByType<PeerDisconnected>().id;
        MsgID_ConnectError = _metaSet.GetByType<PeerConnectError>().id;
        MsgID_SendError = _metaSet.GetByType<PeerSendError>().id;
        MsgID_RecvError = _metaSet.GetByType<PeerRecvError>().id;
    }

    HashSet<string> _group = new HashSet<string>();

    // 鼓励使用RegisterGroup+Add, 而不是Add+Remove
    public bool RegisterGroup(string name)
    {
        if (_group.Contains(name))
            return false;

        _group.Add(name);

        return true;
    }

    /// <summary>
    /// 发一个消息
    /// </summary>
    /// <typeparam name="T">消息类型</typeparam>
    /// <param name="msg">消息内容</param>

    public void SendMessage<T>(T msg, bool isShow = true)
    {
        if (_socket == null)
            return;

        uint msgID = _metaSet.GetByType<T>().id;

        if (msgID == 0)
        {
            throw new InvalidCastException("Error when getting msgID:" + typeof(T).FullName);
        }


        MemoryStream data = new MemoryStream();

        try
        {
            IMessage im = msg as IMessage;
            im.WriteTo(data);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return;
        }

        _socket.SendPacket(msgID, data.ToArray());
    }

    /// <summary>
    /// 手工投递一个消息
    /// </summary>
    /// <typeparam name="T">消息类型</typeparam>
    /// <param name="msg">消息内容</param>\

    public void PostMessage<T>(T msg)
    {
        var meta = _metaSet.GetByType<T>();

        if (meta == MessageMetaSet.NullMeta)
        {
            Debug.LogError("未注册的消息: " + typeof(T).FullName);
            return;
        }

        MemoryStream data = new MemoryStream();

        IMessage im = msg as IMessage;
        im.WriteTo(data);

        PostStream(meta.id, data);
    }
    /// <summary>
    /// 注册一个消息
    /// </summary>
    /// <typeparam name="T">消息类型</typeparam>
    /// <param name="callback">回调处理</param>

    public void RegisterMessage<T>(Action<object> callback)
    {
        var meta = _metaSet.GetByType<T>();
        if (meta == MessageMetaSet.NullMeta)
        {
            Debug.LogError("未注册的消息:" + typeof(T).FullName);
            return;
        }

        _dispatcher.Add(meta.id, callback);
    }

    public bool CheckMessageExist<T>()
    {
        var meta = _metaSet.GetByType<T>();
        if (meta == MessageMetaSet.NullMeta)
            return false;
        if (_dispatcher.Contain(meta.id))
            return true;
        return false;
    }

    public void UnRegisterMessage<T>(Action<object> callback)
    {
        var meta = _metaSet.GetByType<T>();
        if (meta == MessageMetaSet.NullMeta)
        {
            Debug.LogError("未注册的消息:" + typeof(T).FullName);
            return;
        }

        _dispatcher.Remove(meta.id, callback);
    }

    //注销所有回调函数
    public void ResetRegisterMessage()
    {
        _dispatcher.ClearMsg();
    }

    protected override void ProcessStream(uint msgid, MemoryStream stream)
    {
        //if (stream == null)
        //   return;

        try
        {
            var meta = _metaSet.GetByID(msgid);
            if (meta != MessageMetaSet.NullMeta)
            {
                //var msg = Activator.CreateInstance(meta.type.GetType());
                //meta.type.GetType() msg = new(meta.type);// as IMessage;
                //object msg = Activator.CreateInstance(meta.type);

                object result = null;
                Type type = Type.GetType(meta.type.FullName);

                result = Activator.CreateInstance(type);
                IMessage im = result as IMessage;
                if (stream != null)
                    im.MergeFrom(stream);

                _dispatcher.Invoke(msgid, im);
            }
        }
        catch (Exception e)
        {
            //uint id = msgid;

            Debug.LogError(e.ToString());
        }
    }

    public ClientSocket GetSocket()
    {
        return _socket;
    }



    public bool IsConnect()
    {
        if (_socket != null && _socket.IsConnected)
            return true;
        return false;
    }

}
