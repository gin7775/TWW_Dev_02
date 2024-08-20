using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtaqueTarik : StateMachineBehaviour
{
    
        ContainerTarik container;
        Transform playerTransform;
      
        
        NavMeshAgent agent;
     
     
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            playerTransform = GameObject.FindWithTag("Player").transform;
            container = animator.GetComponent<ContainerTarik>();
            
            agent = animator.GetComponent<NavMeshAgent>();
            
           
        
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(new Vector3(playerTransform.position.x, animator.transform.position.y, playerTransform.position.z));

        if (Vector3.Distance(animator.transform.position, playerTransform.position) <= container.jumpTriggerDistance)
        {
            animator.SetTrigger("Saltar");
        }
        

        if (Vector3.Distance(animator.transform.position, playerTransform.position) > 10f)
        {
            animator.SetTrigger("Patrullaje");
            container.animatorTarik.SetTrigger("Patrol");

        }
        else
        {
            container.animatorTarik.SetTrigger("Attack");
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       
    }

    

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

