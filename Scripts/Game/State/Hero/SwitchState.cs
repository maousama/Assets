using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchState : HeroState
{

    public SwitchState(ICharacter character) : base(character) { }

    public override string Name
    {
        get
        {
            return "SwitchState";
        }
    }


}