using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : HeroState
{
    private float h, v;
    private Vector3 rollPos;

    GhostObjectInfo ghostObjectInfo;
    public RollState(ICharacter character) : base(character) { }
    public override string Name
    {
        get
        {
            return "RollState";
        }
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        h = ownerAnimator.GetFloat("horizontal");
        v = ownerAnimator.GetFloat("vertical");
        if (h == 0 && v == 0)
        {
            v = 1;
        }
        rollPos = owner.transform.forward * v + owner.transform.right * h;
        rollPos = rollPos.normalized;
        ghostObjectInfo = MeshGhostCreater.Instance.BeginCreateGhost(owner.transform.GetChild(0), 0.5f, 0.1f, Color.black, 1f);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        MeshGhostCreater.Instance.StopCreateGhost(ghostObjectInfo);
    }

    public override void StateLoop()
    {
        base.StateLoop();
        if (h == 0 && v == 1)
        {
            ownerAnimator.SetFloat("vertical", 1);
        }
        owner.GetComponent<CharacterController>().Move(rollPos * 0.1f);
    }
}
