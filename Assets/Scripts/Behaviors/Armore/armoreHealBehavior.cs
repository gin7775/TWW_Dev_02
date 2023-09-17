using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armoreHealBehavior : StateMachineBehaviour
{
    private Armore armore;

    public float HealingTimer = 5;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore = animator.gameObject.GetComponent<Armore>();
        armore.guardState = 3;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HealingTimer -= Time.deltaTime;
        if (HealingTimer <= 0 )
        {
            armore.currentHealth += 50;
            if(armore.currentHealth >= armore.maxHealth)
            {
                armore.currentHealth = armore.maxHealth;
            }
            if(armore.poiseHealth > 0)
            {
                animator.SetTrigger("HitMe");
            }
            else
            {
                animator.SetTrigger("HealBreak");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HealingTimer = 5;
        armore.guardState = 1;
    }

   
}
