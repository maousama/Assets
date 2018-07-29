using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : HeroState
{
    public DieState(ICharacter character) : base(character) { }
    public override string Name
    {
        get
        {
            return "DieState";
        }
    }

    public override void OnStateEnter()
    {
        owner.GetComponent<Animator>().SetTrigger("die");
    }

    public override void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void StateLoop()
    {
        throw new System.NotImplementedException();
    }
}
