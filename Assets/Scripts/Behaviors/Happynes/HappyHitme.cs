using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyHitme : StateMachineBehaviour
{
    public float hitTimer;


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitTimer -= Time.deltaTime;
        if (hitTimer <= 0)
        {
            animator.SetTrigger("Action");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitTimer = 2;
    }

   
}
