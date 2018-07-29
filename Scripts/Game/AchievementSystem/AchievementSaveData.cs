using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSaveData : MonoBehaviour
{

    public int EnemyKilledCount { get; set; }
    public int WeaponCollection { get; set; }
    public int TrapTriggeringCount { get; set; }

    //储存记录
    public void SaveData()
    {
        PlayerPrefs.SetInt("EnemyKilledCount ", EnemyKilledCount);
        PlayerPrefs.SetInt("WeaponCollection ", WeaponCollection);
        PlayerPrefs.SetInt("TrapTriggeringCount ", TrapTriggeringCount);

    }
    //需要恢复的记录
    public void LoadData()
    {
        EnemyKilledCount = PlayerPrefs.GetInt("EnemyKilledCount ", 0);
        WeaponCollection = PlayerPrefs.GetInt("WeaponCollection ", 0);
        TrapTriggeringCount = PlayerPrefs.GetInt("TrapTriggeringCount ", 0);


    }

}
