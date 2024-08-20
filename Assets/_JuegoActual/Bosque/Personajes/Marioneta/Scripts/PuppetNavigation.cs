using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuppetNavigation : StateMachineBehaviour
{
    public Transform destination;
    NavMeshAgent puppet;
    ContenedorPuppet contenedorPuppet;
    public float distance;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puppet = animator.gameObject.GetComponent<NavMeshAgent>();
        destination = animator.gameObject.GetComponent<Puppet>().player;
        contenedorPuppet = animator.gameObject.GetComponent<ContenedorPuppet>();
        contenedorPuppet.animPuppet.SetBool("Walk", true);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puppet.speed = 1.5f;
        puppet.destination = destination.position;
        if (Vector3.Distance(puppet.transform.position, destination.position) <= distance)
        {
            animator.SetTrigger("Attack");
            contenedorPuppet.animPuppet.SetTrigger("Attack");
           
        }
    }
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedorPuppet.animPuppet.SetBool("Walk", false);
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
