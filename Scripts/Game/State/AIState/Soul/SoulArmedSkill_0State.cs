using UnityEngine;
using System.Collections;

public class SoulArmedSkill_0State : SoulState
{
    public SoulArmedSkill_0State(ICharacter character) : base(character) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

    }

    public override string Name
    {
        get
        {
            return "SoulArmedSkill_0State";
        }
    }
}
