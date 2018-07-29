using UnityEngine;
using System.Collections;

public class SoulAnimAndStateManager : AnimAndStateManager
{

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Soul soul = animator.transform.GetComponent<Soul>();
        ClearAllTrigger(animator);

        #region Run Or Idle
        if (stateInfo.IsName("Armed-Idle"))
        {
            soul.CurrentState.ChangeState(soul.SoulIdleState);
            ClearAllTrigger(animator);
        }
        else if (stateInfo.IsName("Armed-Run"))
        {
            soul.CurrentState.ChangeState(soul.SoulRunState);
            ClearAllTrigger(animator);
        }
        #endregion


        #region Attack
        else if (stateInfo.IsName("Armed-Activate-Left"))
        {
            soul.CurrentState.ChangeState(soul.SoulArmedAttack_0State);
            ClearAllTrigger(animator);
        }
        else if (stateInfo.IsName("Armed-Activate-Right"))
        {
            soul.CurrentState.ChangeState(soul.SoulArmedAttack_1State);
            ClearAllTrigger(animator);
        }
        else if (stateInfo.IsName("Armed-Attack-Dual2"))
        {
            soul.CurrentState.ChangeState(soul.SoulArmedSkill_0State);
            ClearAllTrigger(animator);
        }
        #endregion


        #region Roll
        //else if (stateInfo.IsName("Unarmed-Roll")|| stateInfo.IsName("2Hand-Axe-Roll"))
        //{
        //    hero.CurrentState.ChangeState(hero.unarmedRollState);
        //    ClearAllTrigger(animator);
        //}
        #endregion


        #region GetHit
        else if (stateInfo.IsName("Armed-GetHit-F1"))
        {
            soul.CurrentState.ChangeState(soul.SoulInjuredState);
            ClearAllTrigger(animator);
        }
        #endregion


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

