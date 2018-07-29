using UnityEngine;
using System.Collections;

public abstract class SoulState : IAIState
{
    public SoulState(ICharacter character) : base(character) { }

    public override void StateLoop()
    {
        base.StateLoop();
    }
}
