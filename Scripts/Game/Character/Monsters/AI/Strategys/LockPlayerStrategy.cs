using UnityEngine;
using System.Collections;
using System;

public class LockPlayerStrategy : IStrategy
{
    public LayerMask layerMask = LayerMask.GetMask("Player");

    public override StrategyType StrategyType
    {
        get
        {
            return StrategyType.LockPlayerStrategy;
        }
    }

    public LockPlayerStrategy(AI aI) : base(aI) { }



    public override void StrategyLoop()
    {
        //检测执行攻击指令
        for (int i = 0; i < aI.Owner.curCheckAtkInfoStrList.Count; i++)
        {
            if (aI.Owner.allAttackInfoDic[aI.Owner.curCheckAtkInfoStrList[i]].RangeChecker.Check(layerMask).Count != 0)
            {
                aI.Owner.inputManager.inputBoolDic[aI.Owner.allAttackInfoDic[aI.Owner.curCheckAtkInfoStrList[i]].InputStr] = true;
                return;
            }
        }
        //愤怒破拆条件达成或无寻路信息时转为破拆状态
        if (IsObscuredByBuildingAndRage())
        {
            ChangeStrategy(aI.LockBuildingStrategy);
            aI.curBuildingTarget = aI.sceneRaycastHit[0].collider.GetComponent<IBuilding>();
            return;
        }
        //if (IsNoAStarWay())
        //{
        //    if (aI.sceneRaycastHit != null)
        //    {
        //        if (aI.sceneRaycastHit.Length != 0)
        //        {
        //            ChangeStrategy(aI.LockBuildingStrategy);
        //            aI.curBuildingTarget = aI.sceneRaycastHit[0].collider.GetComponent<IBuilding>();
        //            return;
        //        }
        //    }
        //    RunStraightToPlayerTarget();
        //    return;
        //}
        //愤怒破拆状态未达成并且有路根据寻路信息寻路
        else if (aI.aStarDestinationStack != null)
        {
            if (aI.aStarDestinationStack.Count > 0)
            {
                Debug.Log("寻路");
                RunAlongAStarPath();
                return;
            }
        }
    }







    private void RunAlongAStarPath()
    {
        //如果没有的时候则取一个
        if (aI.curDestination == AI.NullVector)
        {
            aI.curDestination = aI.aStarDestinationStack.Pop();
        }
        //到达一个点后取下一个
        if (Vector3.Distance(aI.Owner.transform.position, aI.curDestination) < 0.3f)
        {
            aI.curDestination = aI.aStarDestinationStack.Pop();
            Debug.Log(aI.curDestination);
        }
        Debug.Log("Destination:" + aI.curDestination);
        Vector3 runForward = aI.curDestination - aI.Owner.transform.position;
        Vector3 inputVec = aI.Owner.transform.InverseTransformDirection(runForward);
        inputVec = (inputVec - Vector3.up * inputVec.y).normalized;
        aI.Owner.inputManager.Horizontal = inputVec.x;
        aI.Owner.inputManager.Vertical = inputVec.z;
    }

    //private void RunStraightToPlayerTarget()
    //{
    //    Vector3 runForward = aI.playerTarget.position - aI.Owner.transform.position;
    //    Vector3 inputVec = aI.Owner.transform.InverseTransformDirection(runForward);
    //    inputVec = (inputVec - Vector3.up * inputVec.y).normalized;
    //    aI.Owner.inputManager.Horizontal = inputVec.x;
    //    aI.Owner.inputManager.Vertical = inputVec.z;
    //}












    private bool IsObscuredByBuildingAndRage()
    {
        bool returnValue = false;
        if (aI.sceneRaycastHit != null)
        {
            returnValue = (aI.sceneRaycastHit.Length > 0 && aI.canTakeDownBuildingCount > aI.sceneRaycastHit.Length);
        }

        return returnValue;
    }

    private bool IsNoAStarWay()
    {
        bool returnValue = true;
        if (aI.aStarDestinationStack == null)
        {
            returnValue = true;
        }
        else
        {
            if (aI.aStarDestinationStack.Count == 0)
            {
                returnValue = true;
            }
        }
        return returnValue;
    }
}
