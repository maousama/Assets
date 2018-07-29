using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamedef;
using System.Threading;

public class NetController : SocketBase
{

    public static NetController netCtrlerInstance;
    public string AccountName = "";
    public int RoleId;
    public int GoldNumber;
    public int index = 0;

    protected override void OnAwake()
    {
        base.OnAwake();
        netCtrlerInstance = this;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
    }
    protected override void OnStart()
    {
        base.OnStart();
        DontDestroyOnLoad(this.gameObject);
    }

}
