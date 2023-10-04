using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CorrerEmpresarioStateMachine : StateMachineBehaviour
{
    public Transform destination;
    ContenedorEmpresario contenedorEmpresario;
    NavMeshAgent empresario;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        empresario = animator.gameObject.GetComponent<NavMeshAgent>();
        contenedorEmpresario = animator.gameObject.GetComponent<ContenedorEmpresario>();
        destination = animator.gameObject.GetComponent<EmpresarioScript>().player;
        contenedorEmpresario.animEmpresario.SetBool("Andar", true);
        empresario.speed = 5f;
        contenedorEmpresario.coolDown = 2f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        contenedorEmpresario.coolDownRun -= Time.deltaTime;

        if(contenedorEmpresario.coolDownRun <= 0)
        {
            empresario.speed = 3f;
            contenedorEmpresario.coolDownRun = 2f;
        }
       
       


        Vector3 direction = (destination.position - animator.transform.position).normalized;
        Vector3 fleeDirection = -direction;
        Vector3 fleePosition = animator.transform.position + fleeDirection * 10f;
        empresario.destination = fleePosition;


        if (Vector3.Distance(empresario.transform.position, destination.position) >= 4.5f)
        {
            animator.SetTrigger("Ataque");

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedorEmpresario.animEmpresario.SetBool("Andar", false);
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
