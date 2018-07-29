using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHero : ICharacter
{

    public IMagic magic;

    public override CharacterType CharacterType
    {
        get
        {
            return CharacterType.Hero;
        }
    }

    public override string Name
    {
        get { return "TestHero"; }
    }

    public override void PlayRunSound()
    {

    }

    protected override void InitAllState()
    {

    }

    protected override void InitInputManager()
    {

    }

    protected override void InitSkillDic()
    {

    }

    protected override void OnAwake()
    {

    }

    protected override void OnStart()
    {
    }

    protected override void OnUpdate()
    {

    }

    // Use this for initialization
    void Start()
    {

        var currentMagic = Instantiate(magic);

        currentMagic.owner = this;
        currentMagic.info = new AttackInfo("TestMagic", this, new BoxCastChecker(5, 5, 0, this.transform), 5, "");
        currentMagic.info.DamageRate = 1f;

        magic.transform.position = transform.position;
        magic.transform.forward = transform.forward;
    }


}
