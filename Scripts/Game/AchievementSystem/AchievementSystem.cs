using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem 
{
    
    private AchievementSaveData lastSaveData = null;
    //记录成就项目
    private int enemyKilledCounts = 0;
    private int weaponCollections = 0;
    private int trapTriggeringCounts = 0;
    //注册观察者
    public void Initialize()
    {

    }
    //产生磁盘数据
    public AchievementSaveData CreateSaveData()
    {
        AchievementSaveData saveData = new AchievementSaveData();
        saveData.EnemyKilledCount = Mathf.Max(enemyKilledCounts, lastSaveData.EnemyKilledCount);
        saveData.WeaponCollection = Mathf.Max(weaponCollections, lastSaveData.WeaponCollection);
        saveData.TrapTriggeringCount = Mathf.Max(trapTriggeringCounts, lastSaveData.TrapTriggeringCount);
        
        return saveData;
    }
    public void SetSaveData(AchievementSaveData saveData)
    {
        lastSaveData = saveData;
    }
    public void AddEnemyKilledCount()
    {
        enemyKilledCounts++;
    }
    public void AddweaponCollectionCount()
    {
        weaponCollections++;
    }
    public void AddtrapTriggeringCount()
    {
        trapTriggeringCounts++;
    }
}
