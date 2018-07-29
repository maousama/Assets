using UnityEngine;
using System.Collections;

public class Spike : ITrap
{
    public override string Name
    {
        get
        {
            return "Spike";
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
        ATK = 5f;
    }

    protected override void OnAwake()
    {
        
    }

    protected override void OnFixedUpdate()
    {

    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {

    }

    protected override void OnBroken()
    {

    }

}
