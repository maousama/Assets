using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulArmedAttack_0State : SoulState
{
    public SoulArmedAttack_0State(ICharacter character) : base(character)
    {
        skillInfoList.Add(owner.allAttackInfoDic["explore"]);
        skillInfoList.Add(owner.allAttackInfoDic["attack_1"]);
    }

    public override string Name
    {
        get
        {
            return "SoulArmedAttack_0State";
        }
    }

    public override void StateLoop()
    {
        base.StateLoop();
        Debug.Log(ownerInputManager.inputBoolDic.Count);
        if (ownerInputManager.inputBoolDic["Fire1"])
        {
            ownerAnimator.SetTrigger("attack_1");
        }
        if (ownerInputManager.Horizontal != 0 || ownerInputManager.Vertical != 0)
        {
            owner.GetComponent<CharacterController>().Move(ownerInputManager.Horizontal * 0.01f * owner.transform.right + ownerInputManager.Vertical * 0.01f * owner.transform.forward);
            ownerAnimator.SetBool("run", true);
        }
    }
}
