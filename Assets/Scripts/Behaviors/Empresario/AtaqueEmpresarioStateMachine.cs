using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtaqueEmpresarioStateMachine : StateMachineBehaviour
{
    public Transform player;
    ContenedorEmpresario contenedorEmpresario;
    NavMeshAgent empresario;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        empresario = animator.gameObject.GetComponent<NavMeshAgent>();
        contenedorEmpresario = animator.gameObject.GetComponent<ContenedorEmpresario>();
        contenedorEmpresario.projectilesLock = true;
        player = animator.gameObject.GetComponent<EmpresarioScript>().player;
        contenedorEmpresario.coolDownAnim = false;
       
        contenedorEmpresario.animEmpresario.SetBool("Andar", false);
        contenedorEmpresario.animEmpresario.SetTrigger("ataque");
        contenedorEmpresario.projectilesLock = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        empresario.speed = 0f;
        
         
       
        animator.transform.LookAt(new Vector3(player.position.x, animator.transform.position.y, player.position.z));

        

        if (Vector3.Distance(empresario.transform.position, player.position) >= 13f)
        {
            animator.SetTrigger("Patrol");

        }

        if (Vector3.Distance(empresario.transform.position, player.position) <= 4f)
        {
            animator.SetTrigger("Correr");

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        contenedorEmpresario.coolDown = 2f;

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
