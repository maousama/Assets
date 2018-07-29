using UnityEngine;
using System.Collections;

public class Bonfire : ITrap
{
    public override string Name
    {
        get
        {
            return "Bonfire";
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
        HP = 10f;
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
