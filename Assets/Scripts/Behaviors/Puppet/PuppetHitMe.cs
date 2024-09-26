using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuppetHitMe : StateMachineBehaviour
{
    NavMeshAgent puppet;
    
    ContenedorPuppet contenedorPuppet;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puppet = animator.gameObject.GetComponent<NavMeshAgent>();
        contenedorPuppet = animator.gameObject.GetComponent<ContenedorPuppet>();

        
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        puppet.speed = 0f;
        contenedorPuppet.waitingTimeHitme -= Time.deltaTime;

        if(contenedorPuppet.waitingTimeHitme <= 0)
        {
            animator.SetTrigger("Navigation");

            contenedorPuppet.projectilesLock = true;
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedorPuppet.waitingTimeHitme = 2f;
        contenedorPuppet.animPuppet.SetBool("Idle", false);

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
