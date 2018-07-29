using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Scener
{
    public Scener(AI aI)
    {
        this.aI = aI;
    }
    private Scener() { }
    private AI aI;



    /// <summary>
    /// A星寻路工具在AI层根据怪物体型初始化
    /// </summary>
    public AStarPathFindingTool aStarPathFindingTool;
    /// <summary>
    /// AI的检查更新循环的具体方法
    /// </summary>
    public void ScenerLoop()
    {
        float curDisFormPlayerTarget = Vector3.Distance(aI.playerTarget.transform.position, aI.Owner.transform.position);
        aI.sceneRaycastHit = Physics.RaycastAll(aI.Owner.transform.position + Vector3.up * 0.5f, (aI.playerTarget.transform.position - aI.Owner.transform.position), curDisFormPlayerTarget, aI.sceneCheckRayLayerMask);
        Debug.DrawLine(aI.Owner.transform.position + Vector3.up * 0.5f, aI.playerTarget.transform.position + Vector3.up * 0.5f, Color.blue);
        aI.canTakeDownBuildingCount = Convert.ToInt32(aI.Owner.Rage / 10f);
        //只有当中间有障碍遮挡并且怪物未进入破拆模式的时候调用AStar
        if (aI.sceneRaycastHit.Length > aI.canTakeDownBuildingCount)
        {
            aI.aStarDestinationStack = aStarPathFindingTool.SumPathFromChunk(aI.Owner.InRoom.MapChunk, aI.Owner.transform.position, aI.playerTarget.position, 3f);
            aI.curDestination = AI.NullVector;
        }
        else
        {
            if (aI.aStarDestinationStack != null)
            {
                aI.aStarDestinationStack.Clear();
            }
        }
    }
}
