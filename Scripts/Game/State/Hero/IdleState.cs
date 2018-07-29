using UnityEngine;
using System.Collections;

public class IdleState : HeroState
{
    public IdleState(ICharacter character) : base(character) { }

    public override string Name
    {
        get
        {
            return "IdleState";
        }
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    public override void StateLoop()
    {
        base.StateLoop();
        if (ownerInputManager.Horizontal != 0 || ownerInputManager.Vertical != 0)
        {
            ownerAnimator.SetBool("run", true);
        }
        if (ownerInputManager.inputBoolDic["Fire1"])
        {
            ownerAnimator.SetTrigger("attack_0");
        }
        if (ownerInputManager.inputBoolDic["Switch"])
        {
            if (owner.OwnWeapon != null)
            {
                ownerAnimator.SetTrigger("switch");
            }
        }
    }
}
