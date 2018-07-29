using UnityEngine;
using System.Collections;

public class PoisonCauldron : ITrap
{
    public override string Name
    {
        get
        {
            return "Cauldron";
        }
    }

    public override float Weight
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    protected override void InitAttributes()
    {
        HP = 10;
        ATK = 2f;
    }

    protected override void OnAwake() { }

    protected override void OnFixedUpdate() { }

    protected override void OnStart() { }

    protected override void OnUpdate() { }

    protected override void OnBroken()
    {

    }

}
