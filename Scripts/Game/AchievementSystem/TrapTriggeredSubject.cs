using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriggeredSubject : ISubject {

    private int trapTriggingCount = 0;
    private ITrapTrigger trapTrigging = null;
    public TrapTriggeredSubject() { }
    //获得对象
    public ITrapTrigger GetTrapTrigging()
    {
        return trapTrigging;
    }
    //当前陷阱触发数
    public int GetTrapTriggingCount()
    {
        return trapTriggingCount;
    }
    public override void SetParam(Object Param)
    {
        base.SetParam(Param);
        trapTrigging = Param as ITrapTrigger;
        trapTriggingCount++;
        Notify();

    }
}
