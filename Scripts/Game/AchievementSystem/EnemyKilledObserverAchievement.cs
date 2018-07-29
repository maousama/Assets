using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledObserverAchievement : IObserver
{
    //成就观察敌人死亡事件
    private EnemyKilledSubject enemysubject = null;
    private AchievementSystem enemyachievementSystem = null;
     
    public EnemyKilledObserverAchievement(AchievementSystem achievementSystem)
    {
        enemyachievementSystem = achievementSystem;
    }
    //设置观察主题
    public void SetSubject(ISubject subject)
    {
        enemysubject = subject as EnemyKilledSubject;
    }
    public override void ObserveUpdate()
    {
        enemyachievementSystem.AddEnemyKilledCount();
    }
}
