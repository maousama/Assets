using UnityEngine;
using System.Collections;

public class InjuredState : HeroState
{
    public InjuredState(ICharacter character) : base(character) { }
    public override string Name
    {
        get
        {
            return "InjuredState";
        }
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        MeshGhostCreater.Instance.CreateSingleGhost(owner.transform.GetChild(0), 1f, Color.red, 1f);
    }

}
