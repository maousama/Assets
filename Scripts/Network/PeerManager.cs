﻿using UnityEngine;

public class PeerManager : Singleton<PeerManager>
{
    MessageMetaSet _meta;

    public MessageMetaSet MsgMeta
    {
        get { return _meta; }
    }

    public PeerManager( )
    {
        _meta = new MessageMetaSet();
        //_meta.Scan("Gamedef");
        _meta.DefultRegister();
    }

    /// <summary>
    /// 在主摄像机上放置NetworkPeer
    /// </summary>
    /// <param name="name">提前命名</param>
    /// <returns></returns>
    //public SocketAPI Get( string name )
    //{
    //    var cam = Camera.main;
    //    if (cam == null)
    //    {
    //        Debug.LogError("NetworkPeer 必须在主摄像机上");
    //        return null;
    //    }

    //    var peers = cam.GetComponents<SocketAPI>();
    //    for( int i = 0;i< peers.Length;i++)
    //    {
    //        if (peers[i].Name == name)
    //            return peers[i];
    //    }

    //    // Make unity happy
    //    var com = cam.gameObject.AddComponent<SocketAPI>();
    //    com.Name = name;
    //    return com;
    //}
}

