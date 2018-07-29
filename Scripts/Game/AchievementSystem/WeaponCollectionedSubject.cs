using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollectionedSubject : ISubject {

    private int WeaponCollectionCount = 0;
    private IWeaponCollection WeaponCollection = null;
    public WeaponCollectionedSubject() { }
    //获得对象
    public IWeaponCollection GetIWeaponCollection()
    {
        return WeaponCollection;
    }
    //当前武器收集数
    public int GetKilledCount()
    {
        return WeaponCollectionCount;
    }
    public override void SetParam(Object Param)
    {
        base.SetParam(Param);
        WeaponCollection = Param as IWeaponCollection;
        WeaponCollectionCount++;
        Notify();

    }
}
