using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soul : IMonster
{
    public ICharacter player;

    public SoulArmedAttack_0State SoulArmedAttack_0State;
    public SoulArmedAttack_1State SoulArmedAttack_1State;
    public SoulDieState SoulDieState;
    public SoulIdleState SoulIdleState;
    public SoulInjuredState SoulInjuredState;
    public SoulRollState SoulRollState;
    public SoulRunState SoulRunState;
    public SoulArmedSkill_0State SoulArmedSkill_0State;

    public override string Name
    {
        get
        {
            return "Soul";
        }
    }

    public override void PlayRunSound()
    {

    }

    protected override void InitAllState()
    {
        SoulArmedAttack_0State = new SoulArmedAttack_0State(this);
        SoulArmedAttack_1State = new SoulArmedAttack_1State(this);
        SoulDieState = new SoulDieState(this);
        SoulIdleState = new SoulIdleState(this);
        SoulInjuredState = new SoulInjuredState(this);
        SoulRollState = new SoulRollState(this);
        SoulRunState = new SoulRunState(this);
        SoulArmedSkill_0State = new SoulArmedSkill_0State(this);
        CurrentState = SoulIdleState;
    }

    protected override void InitSkillDic()
    {
        LayerMask _layerMask = LayerMask.GetMask("Player");
        allAttackInfoDic.Add("explore", new AttackInfo("explore", this, new CycleChecker(3, _layerMask, transform), 5, "Fire4"));
        allAttackInfoDic.Add("attack_0", new AttackInfo("attack_0", this, new BoxCastChecker(1, 20, _layerMask, transform), 3, "Fire1"));
        allAttackInfoDic.Add("attack_1", new AttackInfo("attack_1", this, new BoxCastChecker(2, 20, _layerMask, transform), 3, "Fire1"));
    }



    protected override void OnStart()
    {
        InitSkillDic();
        InitAllState();
    }

    protected override void OnUpdate()
    {

    }
    protected override void OnAwake()
    {
        base.OnAwake();
    }



}
