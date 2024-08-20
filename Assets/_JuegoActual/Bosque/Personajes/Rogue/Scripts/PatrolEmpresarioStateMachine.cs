using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEmpresarioStateMachine : StateMachineBehaviour
{
    public Transform player;
    ContenedorEmpresario contenedorEmpresario;
    NavMeshAgent empresario;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        empresario = animator.gameObject.GetComponent<NavMeshAgent>();
        player = animator.gameObject.GetComponent<EmpresarioScript>().player;
        contenedorEmpresario = animator.gameObject.GetComponent<ContenedorEmpresario>();
        contenedorEmpresario.maxPosition = contenedorEmpresario.destination.Length - 1;

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedorEmpresario.animEmpresario.SetBool("Andar", true);
        empresario.speed = 2f;
        empresario.destination = contenedorEmpresario.destination[contenedorEmpresario.nextPosition].position;
        if (Vector3.Distance(empresario.transform.position, contenedorEmpresario.destination[contenedorEmpresario.nextPosition].position) <= 2f)
        {
            if (contenedorEmpresario.nextPosition >= contenedorEmpresario.maxPosition)
            {
                contenedorEmpresario.nextPosition = 0;
            }
            else
            {
                contenedorEmpresario.nextPosition++;
            }
            empresario.destination = contenedorEmpresario.destination[contenedorEmpresario.nextPosition].position;
        }


        if (Vector3.Distance(empresario.transform.position, player.position) <= 9f)
        {
            animator.SetTrigger("Ataque");

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
