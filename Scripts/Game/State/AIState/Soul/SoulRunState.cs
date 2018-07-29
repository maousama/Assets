using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulRunState : SoulState
{

    Vector3 targetDir;

    public SoulRunState(ICharacter character) : base(character)
    {
        skillInfoList.Add(owner.allAttackInfoDic["explore"]);
        skillInfoList.Add(owner.allAttackInfoDic["attack_0"]);
    }

    public override string Name
    {
        get
        {
            return "SoulRunState";
        }
    }
    public override void StateLoop()
    {
        base.StateLoop();

        Move();

        if (ownerInputManager.Horizontal != 0 || ownerInputManager.Vertical != 0)
        {
            ownerAnimator.SetBool("run", true);
        }
        if (ownerInputManager.inputBoolDic["Fire1"])
        {
            ownerAnimator.SetTrigger("attack_0");
        }
        if (ownerInputManager.inputBoolDic["Fire4"])
        {
            ownerAnimator.SetTrigger("skill_0");
        }
    }
}
