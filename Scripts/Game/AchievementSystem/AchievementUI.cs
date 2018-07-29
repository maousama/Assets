using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour {

    public Text enemyKilledCount;
    public Text weaponCollection;
    public Text trapTriggeringCount;

    
    public void ShowGameUI(AchievementSaveData saveData)
    {
        enemyKilledCount.text = string.Format("当前杀敌总和：｛0｝", saveData.EnemyKilledCount);
        weaponCollection.text = string.Format("当前解锁武器总和：｛0｝", saveData.WeaponCollection);
        trapTriggeringCount.text = string.Format("当前触发陷阱总和：｛0｝", saveData.TrapTriggeringCount);
        
    }
}
