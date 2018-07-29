using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDieState : SoulState
{
    public SoulDieState(ICharacter character) : base(character) { }

    public override string Name
    {
        get
        {
            return "SoulDieState";
        }
    }
}
