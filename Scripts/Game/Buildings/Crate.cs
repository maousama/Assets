using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : IBuilding
{
    public override string Name
    {
        get
        {
            return "Crate";
        }
    }

    public override float Weight
    {
        get
        {
            return CannotWalkThrough;
        }
    }

    protected override void InitAttributes()
    {
        HP = Random.Range(5, 25);
    }

    protected override void OnAwake() { }

    protected override void OnFixedUpdate() { }

    protected override void OnStart() { }

    protected override void OnUpdate() { }


    protected override void OnBroken()
    {
        
    }
}
