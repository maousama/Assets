using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollectionObserverAchievement : IObserver {

    private WeaponCollectionedSubject weaponCollectionedSubject = null;
    private AchievementSystem weaponCollectionedAchievementSystem = null;

    public WeaponCollectionObserverAchievement(AchievementSystem achievementSystem)
    {
        weaponCollectionedAchievementSystem = achievementSystem;
    }


    //设置观察主题
    public void SetSubject(ISubject subject)
    {
        weaponCollectionedSubject = subject as WeaponCollectionedSubject;
    }

    public override void ObserveUpdate()
    {
        weaponCollectionedAchievementSystem.AddweaponCollectionCount();
    }
}
