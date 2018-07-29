using UnityEngine;
using System.Collections;

public abstract class ITrap : IBuilding
{
    private float atk;
    /// <summary>
    /// 攻击力1
    /// </summary>
    public float ATK
    {
        get
        {
            return atk;
        }

        set
        {
            atk = value;
        }
    }
}
