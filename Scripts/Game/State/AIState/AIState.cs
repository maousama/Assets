using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IAIState : IState
{
    public IAIState(ICharacter character) : base(character) { }
    /// <summary>
    /// 攻击检测Layer层
    /// </summary>
    public LayerMask checkLayerMask;

    public List<AttackInfo> skillInfoList = new List<AttackInfo>();

    public override void StateLoop()
    {
        base.StateLoop();
    }

    protected override void Move()
    {
        base.Move();
        //Rotate();
    }

    
    Vector3 targetDir; 
    void Rotate()
    {
        float rotateSpeed = owner.AGL/10;
        targetDir = ownerInputManager.Horizontal * owner.transform.right + ownerInputManager.Vertical * owner.transform.forward;
        targetDir = targetDir.normalized;


        Vector3 currenDir = Vector3.Lerp(owner.transform.forward, targetDir, rotateSpeed).normalized;

        if (currenDir == Vector3.zero)
        {
            currenDir = Quaternion.AngleAxis(5, Vector3.up) * owner.transform.forward;
        }

        owner.transform.forward = currenDir;
    }

}
