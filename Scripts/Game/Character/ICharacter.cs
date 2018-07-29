using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacter : MonoBehaviour
{
    /// <summary>
    /// float类型变化的委托
    /// </summary>
    /// <param name="beforeValue"></param>
    /// <param name="afterValue"></param>
    public delegate void OnFloatChange(float beforeValue, float afterValue);
    /// <summary>
    /// 角色类型
    /// </summary>
    public abstract CharacterType CharacterType { get; }

    public InputManager inputManager;
    public Animator animator;
    public RoomController InRoom;


    #region 角色状态模式的状态
    /// <summary>
    /// 角色状态对象
    /// </summary>
    private IState currentState;
    /// <summary>
    /// 角色状态对象访问器
    /// </summary>
    public IState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }

    #endregion


    #region 角色属性
    public OnFloatChange OnMaxHPChange;
    [SerializeField, HideInInspector]
    private float maxHP;
    public float MaxHP
    {
        get
        {
            return maxHP;
        }

        set
        {
            maxHP = value;
            OnMaxHPChange?.Invoke(HP, maxHP);
        }
    }

    public OnFloatChange OnHPChange;
    [SerializeField, HideInInspector]
    private float hp;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            OnHPChange?.Invoke(hp, MaxHP);
        }
    }


    public OnFloatChange OnATKChange;
    [SerializeField, HideInInspector]
    private float atk;
    public float ATK
    {
        get
        {
            return atk;
        }

        set
        {
            OnATKChange?.Invoke(atk, value);
            atk = value;
        }
    }

    public OnFloatChange OnAGLChange;
    [SerializeField, HideInInspector]
    private float agl;
    public float AGL
    {
        get
        {
            return agl;
        }
        set
        {
            OnAGLChange?.Invoke(agl, value);
            agl = value;
        }
    }

    public OnFloatChange OnHardChange;
    [SerializeField, HideInInspector]
    private float hard;
    public float Hard
    {
        get
        {
            return hard;
        }

        set
        {
            OnHardChange?.Invoke(hard, value);
            hard = value;
        }
    }

    /// <summary>
    /// 闪避冷却
    /// </summary>
    [SerializeField, HideInInspector]
    private float dodgeCoolDown;
    /// <summary>
    /// 闪避冷却
    /// </summary>
    public float DodgeCoolDown
    {
        get
        {
            return dodgeCoolDown;
        }
        set
        {
            dodgeCoolDown = value;
        }
    }
    private bool canDodge = true;
    /// <summary>
    /// 能否闪避的布尔值
    /// </summary>
    public bool CanDodge
    {
        get { return canDodge; }
        set { canDodge = value; }
    }

    /// <summary>
    /// 冷却开始计时
    /// </summary>
    /// <returns></returns>
    public IEnumerator DodgeCoolDownStart()
    {
        CanDodge = false;
        yield return new WaitForSeconds(DodgeCoolDown);
        CanDodge = true;
    }


    /// <summary>
    /// 名字访问器
    /// </summary>
    public abstract string Name
    {
        get;
    }

    protected virtual void InitAttribute()
    {

    }


    #endregion


    #region 装备相关
    /// <summary>
    /// 拥有的武器
    /// </summary>
    private IWeapon ownWeapon = null;
    /// <summary>
    /// 拥有武器
    /// </summary>
    public IWeapon OwnWeapon
    {
        get
        {
            return ownWeapon;
        }
        protected set
        {
            ownWeapon = value;
        }
    }
    #endregion


    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        inputManager = transform.GetComponent<InputManager>();
        OnAwake();
    }
    protected abstract void OnAwake();

    private void Start()
    {
        OnStart();
    }
    protected abstract void OnStart();

    private void Update()
    {
        OnUpdate();
        if (CurrentState != null)
        {
            CurrentState.StateLoop();
        }
    }


    protected abstract void OnUpdate();


    public abstract void PlayRunSound();


    protected abstract void InitAllState();


    protected abstract void InitSkillDic();


    protected abstract void InitInputManager();


    public Dictionary<string, AttackInfo> allAttackInfoDic = new Dictionary<string, AttackInfo>();
    public List<string> curCheckAtkInfoStrList = new List<string>();
}
