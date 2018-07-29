using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulRollState : SoulState
{
    public SoulRollState(ICharacter character) : base(character) { }

    public override string Name
    {
        get
        {
            return "SoulRollState";
        }
    }

}
