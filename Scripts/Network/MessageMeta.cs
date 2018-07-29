using System;
using System.Collections.Generic;
using System.Reflection;
using Gamedef;

public class MessageMeta
{
    public Type type;
    public uint id;

    public string name
    {
        get { return type.FullName; }
    }

    public MessageMeta(Type t, uint msgid)
    {
        this.type = t;
        this.id = msgid;
    }

    //public uint GetIDByType(Type t)
    //{
    //    //Check NetWork state
    //    if (t == typeof(PeerConnected))
    //        return 1;

    //    if( t == typeof(PeerDisconnected))
    //        return 2;

    //    if (t == typeof(PeerConnectError))
    //        return 3;

    //    if (t == typeof(PeerSendError))
    //        return 4;

    //    if (t == typeof(PeerRecvError))
    //        return 5;


    //    if (t == typeof(PeerRecvError))
    //        return 5;

    //    return 0;
    //}
}



/// <summary>
/// 消息类型与id的映射表
/// </summary>
public class MessageMetaSet
{

    public readonly static MessageMeta NullMeta = new MessageMeta(typeof(MessageMeta), 0);

    Dictionary<uint, MessageMeta> _idmap = new Dictionary<uint, MessageMeta>();
    Dictionary<Type, MessageMeta> _typemap = new Dictionary<Type, MessageMeta>();
    Dictionary<string, MessageMeta> _namemap = new Dictionary<string, MessageMeta>();


    public MessageMetaSet DefultRegister()
    {
        //注册连接状态MSGID
        RegisterMessage(typeof(PeerConnected), 1);
        RegisterMessage(typeof(PeerDisconnected), 2);
        RegisterMessage(typeof(PeerConnectError), 3);
        RegisterMessage(typeof(PeerSendError), 4);
        RegisterMessage(typeof(PeerRecvError), 5);

        //注册登陆过程中的MSGID
        RegisterMessage(typeof(LoginREQ), 202);
        RegisterMessage(typeof(LoginACK), 203);
        RegisterMessage(typeof(RegisterAccountReq), 200);
        RegisterMessage(typeof(RegisterAccountAck),201);
        RegisterMessage(typeof(HeartBeatREQ), 103);
        RegisterMessage(typeof(HeartBeatACK), 104);


        return this;
    }

    /// <summary>
    /// 将消息注册
    /// </summary>
    /// <param name="id"></param>
    /// <param name="t"></param>
    /// 
    void RegisterMessage(Type t, uint msgid)
    {
        if (GetByType(t) != NullMeta)
        {
            return;
            //throw new Exception("重复的消息ID");
        }

        var mi = new MessageMeta(t, msgid);
        mi.id = msgid;
        mi.type = t;

        _idmap.Add(mi.id, mi);
        _typemap.Add(t, mi);
        _namemap.Add(mi.name, mi);
    }



    /// <summary>
    /// 根据ID取到消息的类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public MessageMeta GetByID(uint id)
    {
        MessageMeta t;
        if (_idmap.TryGetValue(id, out t))
        {
            return t;
        }

        return NullMeta;
    }

    /// <summary>
    /// 根据类型取到ID
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public MessageMeta GetByType(Type t)
    {
        MessageMeta mi;
        if (_typemap.TryGetValue(t, out mi))
        {
            return mi;
        }

        return NullMeta;
    }

    public MessageMeta GetByName(string name)
    {
        MessageMeta mi;
        if (_namemap.TryGetValue(name, out mi))
        {
            return mi;
        }

        return NullMeta;
    }

    public MessageMeta GetByType<T>()
    {
        return GetByType(typeof(T));
    }


}
