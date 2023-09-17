using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armoreHealBreak : StateMachineBehaviour
{
    private Armore armore;

    public float breakTimer = 6;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore = animator.gameObject.GetComponent<Armore>();
        armore.ArmoreHealBreack();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        breakTimer -= Time.deltaTime;
        if (breakTimer <= 0)
        {
            armore.ArmoreIdle();
            animator.SetTrigger("Action");

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        breakTimer = 6;
    }

  
}
