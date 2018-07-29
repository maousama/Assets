using UnityEngine;
using System.Collections;

public class AxeAttack_0State : HeroState
{
    public AxeAttack_0State(ICharacter character) : base(character) { }
    public override string Name
    {
        get
        {
            return "AxeAttack_0State";
        }
    }
    public override void StateLoop()
    {
        base.StateLoop();
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
