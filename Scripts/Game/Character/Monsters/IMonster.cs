using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMonster : ICharacter
{
    public float rage = 0;
    /// <summary>
    /// 愤怒值
    /// </summary>
    public float Rage
    {
        get
        {
            return rage;
        }

        set
        {
            rage = value;
        }
    }

    private float rotSpeed = 0f;
    /// <summary>
    /// 旋转速度
    /// </summary>
    public float RotSpeed
    {
        get
        {
            return rotSpeed;
        }

        set
        {
            rotSpeed = value;
        }
    }

    public AI aI;
    /// <summary>
    /// 技能信息列表
    /// </summary>
    public List<AttackInfo> attackInfoList = new List<AttackInfo>();
    /// <summary>
    /// 减少怒气频率
    /// </summary>
    public float reduceRageInternal = 50;
    /// <summary>
    /// 周期减少愤怒值得迭代器
    /// </summary>
    /// <returns></returns>
    public IEnumerator ReduceRagePeriodically()
    {
        yield return new WaitForSeconds(reduceRageInternal);
        Rage -= 10f;
    }


    public override CharacterType CharacterType
    {
        get
        {
            return CharacterType.Monster;
        }
    }


    protected override void InitInputManager()
    {
        if(inputManager != null)
        {
            inputManager = transform.GetComponent<InputManager>();
            inputManager.readInput = false;
        }
    }



    protected override void OnAwake()
    {
        InitInputManager();
    }

}
