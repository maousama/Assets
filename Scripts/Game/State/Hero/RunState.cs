using UnityEngine;
using System.Collections;

public class RunState : HeroState
{
    public RunState(ICharacter character) : base(character) { }
    public override string Name
    {
        get
        {
            return "RunState";
        }
    }


    public override void StateLoop()
    {
        base.StateLoop();

        if (ownerInputManager.Horizontal == 0 && ownerInputManager.Vertical == 0)
        {
            ownerAnimator.SetBool("run", false);
        }
        else
        {
            Move();
        }


        if (ownerInputManager.inputBoolDic["Fire1"])
        {
            ownerAnimator.SetTrigger("attack_0");
        }
    }

}
