using UnityEngine;
using System.Collections;

public class Pillar : IBuilding
{
    public override string Name
    {
        get
        {
            return "Pillar";
        }
    }
    [Header("===== Setting =====")]
    public int MinHP = 30;
    public int MaxHP = 60;



    public override float Weight
    {
        get
        {
            return CannotWalkThrough;
        }
    }

    protected override void InitAttributes()
    {
        HP = Random.Range(MinHP, MaxHP);
    }

    protected override void OnAwake() { }

    protected override void OnFixedUpdate() { }

    protected override void OnStart() { }

    protected override void OnUpdate() { }

    protected override void OnBroken()
    {

    }
}
