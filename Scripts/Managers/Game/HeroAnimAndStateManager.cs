using UnityEngine;
using System.Collections;

public class HeroAnimAndStateManager : AnimAndStateManager
{
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Hero hero = animator.transform.GetComponent<Hero>();
        ClearAllTrigger(animator);

        #region Run Or Idle
        if (stateInfo.IsName("Unarmed-Idle") || stateInfo.IsName("2Hand-Axe-Idle"))
        {
            hero.CurrentState.ChangeState(hero.IdleState);
            ClearAllTrigger(animator);
        }
        else if (stateInfo.IsName("Unarmed-Run") || stateInfo.IsName("2Hand-Axe-Run"))
        {
            hero.CurrentState.ChangeState(hero.RunState);
            ClearAllTrigger(animator);
        }
        #endregion


        #region Attack
        else if (stateInfo.IsName("Unarmed-Attack-R1"))
        {
            hero.CurrentState.ChangeState(hero.unarmedAttack_0State);
            ClearAllTrigger(animator);
        }
        else if (stateInfo.IsName("Unarmed-Attack-L2"))
        {
            hero.CurrentState.ChangeState(hero.unarmedAttack_1State);
            ClearAllTrigger(animator);
        }
        else if (stateInfo.IsName("Unarmed-Attack-R3"))
        {
            hero.CurrentState.ChangeState(hero.unarmedAttack_2State);
            ClearAllTrigger(animator);
        }
        else if (stateInfo.IsName("Unarmed-Attack-Kick-L1"))
        {
            hero.CurrentState.ChangeState(hero.unarmedAttack_3State);
            ClearAllTrigger(animator);
        }

        else if (stateInfo.IsName("2Hand-Axe-Attack2"))
        {
            hero.CurrentState.ChangeState(hero.axeAttack_0State);
            ClearAllTrigger(animator);
        }
        else if (stateInfo.IsName("2Hand-Axe-Attack6"))
        {
            hero.CurrentState.ChangeState(hero.axeAttack_1State);
            ClearAllTrigger(animator);
        }
        else if (stateInfo.IsName("2Hand-Axe-Attack1"))
        {
            hero.CurrentState.ChangeState(hero.axeAttack_2State);
            ClearAllTrigger(animator);
        }
        #endregion


        #region Roll
        else if (stateInfo.IsName("Unarmed-Roll") || stateInfo.IsName("2Hand-Axe-Roll"))
        {
            hero.CurrentState.ChangeState(hero.unarmedRollState);
            ClearAllTrigger(animator);
        }
        #endregion


        #region GetHit
        else if (stateInfo.IsName("Unarmed-GetHit-F1") || stateInfo.IsName("2Hand-Axe-GetHit-F2"))
        {
            hero.CurrentState.ChangeState(hero.unarmedInjuredState);
            ClearAllTrigger(animator);
        }
        #endregion

        else if (stateInfo.IsName("2Hand-Axe-Sheath") || stateInfo.IsName("2Hand-Axe-Unsheath"))
        {
            hero.CurrentState.ChangeState(hero.switchState);
            ClearAllTrigger(animator);
        }
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //ICharacter character = animator.transform.GetComponent<ICharacter>();
        //if (stateInfo.IsName("Unarmed-Attack-R1"))
        //{
        //    character.CurrentState.ChangeState(character.unarmedAttack_0State);
        //    ClearAllTrigger(animator);
        //}
        //if (stateInfo.IsName("Unarmed-Attack-L2"))
        //{
        //    character.CurrentState.ChangeState(character.unarmedAttack_1State);
        //    ClearAllTrigger(animator);
        //}
        //if (stateInfo.IsName("Unarmed-Attack-R3"))
        //{
        //    character.CurrentState.ChangeState(character.unarmedAttack_2State);
        //    ClearAllTrigger(animator);
        //}
        //if (stateInfo.IsName("Unarmed-Attack-Kick-L1"))
        //{
        //    character.CurrentState.ChangeState(character.unarmedAttack_3State);
        //    ClearAllTrigger(animator);
        //}
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMachineEnter is called when entering a statemachine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
    //
    //}

    // OnStateMachineExit is called when exiting a statemachine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
    //
    //}
}
