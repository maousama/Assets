using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledObserverUI : IObserver
{
    private EnemyKilledSubject enemysubject = null;
    

    public EnemyKilledObserverUI ()
    {
        //游戏赋值
    }
        //通知主题更新
    public override void ObserveUpdate()
    {
      
;      //弹窗显示 地方死亡
    }

    //设置观察主题
   public void SetSubject(ISubject subject)
    {
        enemysubject = subject as EnemyKilledSubject;
    }
}
