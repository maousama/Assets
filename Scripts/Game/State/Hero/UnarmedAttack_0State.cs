using UnityEngine;
using UnityEditor;

public class UnarmedAttack_0State : HeroState
{
    public UnarmedAttack_0State(ICharacter character) : base(character) { }

    public override string Name
    {
        get
        {
            return "UnarmedAttack_0State";
        }
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
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