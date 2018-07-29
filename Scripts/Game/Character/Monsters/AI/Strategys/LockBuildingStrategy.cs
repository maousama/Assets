using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LockBuildingStrategy : IStrategy
{
    public LockBuildingStrategy(AI aI) : base(aI) { }
    public LayerMask layerMask = LayerMask.GetMask("Building");

    public override StrategyType StrategyType
    {
        get
        {
            return StrategyType.LockBuildingStrategy;
        }
    }

    public override void StrategyLoop()
    {
        //检测执行攻击指令
        for (int i = 0; i < aI.Owner.curCheckAtkInfoStrList.Count; i++)
        {
            List<Transform> lookTrList = aI.Owner.allAttackInfoDic[aI.Owner.curCheckAtkInfoStrList[i]].RangeChecker.Check(layerMask);
            if (lookTrList.Count != 0)
            {
                if (lookTrList[i].GetComponent<IBuilding>() == aI.curBuildingTarget)
                {
                    aI.Owner.inputManager.inputBoolDic[aI.Owner.allAttackInfoDic[aI.Owner.curCheckAtkInfoStrList[i]].InputStr] = true;
                }
                return;
            }
        }
        //判断看到玩家则将目标锁定为玩家
        if (IsLookAtPlayer())
        {
            aI.Strategy.ChangeStrategy(aI.LockPlayerStrategy);
            return;
        }
        //向建筑目标移动
        else
        {
            RunToBuildingTarget();
            return;
        }
    }



    private void RunToBuildingTarget()
    {
        if (aI.curBuildingTarget != null)
        {
            Vector3 runForward = aI.curBuildingTarget.transform.position - aI.Owner.transform.position;
            Vector3 inputVec = aI.Owner.transform.InverseTransformDirection(runForward);
            inputVec = (inputVec - Vector3.up * inputVec.y).normalized;
            aI.Owner.inputManager.Horizontal = inputVec.x;
            aI.Owner.inputManager.Vertical = inputVec.z;
        }
        else
        {
            Debug.Log("curBuildingTarget Not Found!");
        }
    }

    private bool IsLookAtPlayer()
    {
        bool returnValue = aI.sceneRaycastHit.Length == 0;
        return returnValue;
    }
}
