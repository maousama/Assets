using UnityEngine;
using System.Collections;

public class Chest : IBuilding
{
    public override string Name
    {
        get
        {
            return "Chest" + level;
        }
    }

    public override float Weight
    {
        get
        {
            return CannotWalkThrough;
        }
    }

    public int level;


    protected override void InitAttributes()
    {
        switch (level)
        {
            case 0:
                HP = 10;
                break;
            case 1:
                HP = 20;
                break;
            case 2:
                HP = 30;
                break;
            case 3:
                HP = 40;
                break;
            case 4:
                HP = 50;
                break;
        }
    }


    protected override void OnAwake() { }


    protected override void OnFixedUpdate() { }


    protected override void OnStart() { }


    protected override void OnUpdate() { }


    protected override void OnBroken()
    {
        switch (level)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

}
