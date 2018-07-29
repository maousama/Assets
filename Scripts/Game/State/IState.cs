using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    public IState(ICharacter character)
    {
        owner = character;
        ownerAnimator = owner.GetComponent<Animator>();
        ownerInputManager = owner.inputManager;
    }
    private IState()
    {

    }

    /// <summary>
    /// 状态的主人
    /// </summary>
    protected ICharacter owner;
    /// <summary>
    /// 状态对应的状态机
    /// </summary>
    protected Animator ownerAnimator;
    /// <summary>
    /// 主人的输入控制
    /// </summary>
    protected InputManager ownerInputManager;
    /// <summary>
    /// 名字
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// 状态循环
    /// </summary>
    public virtual void StateLoop()
    {
        //Debug.Log(Name);
    }
    /// <summary>
    /// 进入该状态
    /// </summary>
    public virtual void OnStateEnter()
    {

    }
    /// <summary>
    /// 状态退出
    /// </summary>
    public virtual void OnStateExit()
    {

    }

    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeState(IState nextState)
    {
        //Debug.Log(nextState);
        owner.CurrentState.OnStateExit();
        owner.CurrentState = nextState;
        owner.CurrentState.OnStateEnter();
        //Debug.Log("Cur:" + owner.CurrentState);
    }


    protected virtual void Move()
    {
        Vector3 moveDir = ownerInputManager.Horizontal * owner.transform.right + ownerInputManager.Vertical * owner.transform.forward;
        owner.GetComponent<CharacterController>().SimpleMove(moveDir * owner.AGL);
    }

}
