using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAndStateManager : StateMachineBehaviour
{

    //public string name;
    public void ClearAllTrigger(Animator animator)
    {
        for (int i = 0; i < animator.parameterCount; i++)
        {
            if (animator.parameters[i].type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(animator.parameters[i].name);
            }
        }
    }

}
