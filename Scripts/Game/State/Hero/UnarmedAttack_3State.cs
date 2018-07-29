using UnityEngine;
using System.Collections;

public class UnarmedAttack_3State : HeroState
{
    public UnarmedAttack_3State(ICharacter character) : base(character) { }

    public override string Name
    {
        get
        {
            return "UnarmedAttack_3State";
        }
    }


    public override void StateLoop()
    {
        base.StateLoop();
        if (ownerInputManager.inputBoolDic["Fire1"])
        {
            ownerAnimator.SetTrigger("attack_0");
        }
        if (ownerInputManager.Horizontal != 0 || ownerInputManager.Vertical != 0)
        {
            owner.GetComponent<CharacterController>().Move(ownerInputManager.Horizontal * 0.01f * owner.transform.right + ownerInputManager.Vertical * 0.01f * owner.transform.forward);
            ownerAnimator.SetBool("run", true);
        }
    }
}
