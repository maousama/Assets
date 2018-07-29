using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTiggerObserverAchievement : IObserver {

    private TrapTriggeredSubject trapTriggeredsubject = null;
    private AchievementSystem trapTriggeredachievementSystem = null;

    public TrapTiggerObserverAchievement(AchievementSystem achievementSystem)
    {
        trapTriggeredachievementSystem = achievementSystem;
    }
    //设置观察主题
    public void SetSubject(ISubject subject)
    {
        trapTriggeredsubject = subject as TrapTriggeredSubject;
    }
    public override void ObserveUpdate()
    {
        trapTriggeredachievementSystem.AddtrapTriggeringCount();
    }
}
