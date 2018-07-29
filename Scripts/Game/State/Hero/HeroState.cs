using UnityEngine;
using System.Collections;

public abstract class HeroState : IState
{
    public HeroState(ICharacter character) : base(character) { }

    public override void StateLoop()
    {
        base.StateLoop();
        ownerAnimator.SetFloat("horizontal", ownerInputManager.Horizontal);
        ownerAnimator.SetFloat("vertical", ownerInputManager.Vertical);
        if (ownerInputManager.inputBoolDic["Fire3"] && owner.CanDodge && Name != "UnarmedInjuredState")
        {
            ownerAnimator.SetTrigger("roll");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ownerAnimator.SetTrigger("injured");
        }
    }
}
