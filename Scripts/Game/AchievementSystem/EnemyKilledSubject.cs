using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledSubject : ISubject
{
    private int killedCount = 0;
    private IEnemy enemy = null;
    public EnemyKilledSubject() { }
    //获得对象
    public IEnemy GetIenemy()
    {
        return enemy;
    }
    //当前BOSS阵亡数
    public int GetKilledCount()
    {
        return killedCount;
    }
    public override void SetParam(Object Param)
    {
        base.SetParam(Param);
        enemy = Param as IEnemy;
        killedCount++;
        Notify();

    }

}
