using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgGame {

    AchievementSystem achievementSystem = new AchievementSystem();
    
    private void SaveData()
    {
        AchievementSaveData saveData = achievementSystem.CreateSaveData();
        saveData.SaveData();
       
    }
    private AchievementSaveData LoadData()
    {
        AchievementSaveData oldData = new AchievementSaveData();
        oldData.LoadData();
        achievementSystem.SetSaveData(oldData);
        return oldData;
    }
}
