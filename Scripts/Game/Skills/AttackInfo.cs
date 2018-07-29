using UnityEngine;
using System.Collections;

public class AttackInfo
{
    public AttackInfo(string name, ICharacter owner, RangeChecker rangeChecker, float coolDown, string inputStr)
    {
        Name = name;
        Owner = owner;
        RangeChecker = rangeChecker;
        CoolDown = coolDown;
        InputStr = inputStr;
    }


    private string name;
    private ICharacter owner;
    private RangeChecker rangeChecker;
    private bool canUse = true;
    private float coolDown = 0f;
    private string inputStr = "";
    private float damageRate=1f;

    public float CoolDown
    {
        get
        {
            return coolDown;
        }

        set
        {
            coolDown = value;
        }
    }

    public RangeChecker RangeChecker
    {
        get
        {
            return rangeChecker;
        }

        set
        {
            rangeChecker = value;
        }
    }

    public ICharacter Owner
    {
        get
        {
            return owner;
        }

        set
        {
            owner = value;
        }
    }

    public string InputStr
    {
        get
        {
            return inputStr;
        }

        set
        {
            inputStr = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public bool CanUse
    {
        get
        {
            return canUse;
        }

        set
        {
            canUse = value;
        }
    }

    public float DamageRate
    {
        get
        {
            return damageRate;
        }

        set
        {
            damageRate = value;
        }
    }
}
