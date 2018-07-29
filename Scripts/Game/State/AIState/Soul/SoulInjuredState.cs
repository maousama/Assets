using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulInjuredState : SoulState
{
    public SoulInjuredState(ICharacter character) : base(character) { }

    public override string Name
    {
        get
        {
            return "SoulInjuredState";
        }
    }
}
