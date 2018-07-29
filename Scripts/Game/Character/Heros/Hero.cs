using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InputManager))]
public class Hero : ICharacter
{
    public Transform backWeaponTr, handWeaponTr;


    public IdleState IdleState;
    public RunState RunState;
    public UnarmedAttack_0State unarmedAttack_0State;
    public UnarmedAttack_1State unarmedAttack_1State;
    public UnarmedAttack_2State unarmedAttack_2State;
    public UnarmedAttack_3State unarmedAttack_3State;
    public RollState unarmedRollState;
    public InjuredState unarmedInjuredState;
    public AxeAttack_0State axeAttack_0State;
    public AxeAttack_1State axeAttack_1State;
    public AxeAttack_2State axeAttack_2State;
    public SwitchState switchState;

    public override CharacterType CharacterType
    {
        get
        {
            return CharacterType.Hero;
        }
    }

    public override string Name
    {
        get
        {
            return "";
        }
    }

    public override void PlayRunSound()
    {
        
    }

    protected override void InitAllState()
    {
        IdleState = new IdleState(this);
        RunState = new RunState(this);
        unarmedAttack_0State = new UnarmedAttack_0State(this);
        unarmedAttack_1State = new UnarmedAttack_1State(this);
        unarmedAttack_2State = new UnarmedAttack_2State(this);
        unarmedAttack_3State = new UnarmedAttack_3State(this);
        unarmedRollState = new RollState(this);
        unarmedInjuredState = new InjuredState(this);
        axeAttack_0State = new AxeAttack_0State(this);
        axeAttack_1State = new AxeAttack_1State(this);
        axeAttack_2State = new AxeAttack_2State(this);
        switchState = new SwitchState(this);
        CurrentState = IdleState;
    }


    protected override void InitInputManager()
    {
        inputManager = transform.GetComponent<InputManager>();
        inputManager.readInput = true;
        
    }

    protected override void OnAwake()
    {
        InitInputManager();
        InitAllState();
        CheckWeaponOnBody();
    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {
        
    }
    
    public void SwitchWeapon()
    {
        CheckWeaponOnBody();
        Debug.Log(animator);
        animator.SetBool("axe", true);
        OwnWeapon.transform.SetParent(handWeaponTr);
        OwnWeapon.transform.localPosition = Vector3.zero;
        OwnWeapon.transform.localRotation = Quaternion.identity;
        
    }
    public void SwitchHand()
    {
        CheckWeaponOnBody();
        animator.SetBool("axe", false);
        OwnWeapon.transform.SetParent(backWeaponTr);
        OwnWeapon.transform.localPosition = Vector3.zero;
        OwnWeapon.transform.localRotation = Quaternion.identity;
        
    }

    public IWeapon CheckWeaponOnBody()
    {
        IWeapon weaponOnBack = backWeaponTr.GetComponentInChildren<IWeapon>();
        IWeapon weaponOnHand = handWeaponTr.GetComponentInChildren<IWeapon>();
        if (weaponOnBack != null)
        {
            OwnWeapon = weaponOnBack;
        }
        if(weaponOnHand != null)
        {
            OwnWeapon = weaponOnHand;
        }
        return OwnWeapon;
    }

    protected override void InitSkillDic()
    {
        
    }
}
