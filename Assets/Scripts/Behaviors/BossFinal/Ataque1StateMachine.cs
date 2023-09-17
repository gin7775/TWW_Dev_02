using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ataque1StateMachine : StateMachineBehaviour
{

    ContenedorFinalBoss contenedor;
    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        agent = animator.gameObject.GetComponent<NavMeshAgent>();

        contenedor = animator.gameObject.GetComponent<ContenedorFinalBoss>();
        contenedor.currentTimeAtaque1 = contenedor.startingTimeAtaque1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        agent.speed = 4.5f;
        agent.destination = contenedor.player.transform.position;

        Vector3 posSpawn = animator.transform.position + new Vector3 (0, 1f, 0);

        if (agent.remainingDistance <= 2f)
        {
            //agent.speed = 0f;

            contenedor.anim.GetComponent<Animator>().SetBool("Combo3", true);

            agent.speed = 3f;
            contenedor.ataque1 = true;
          
        }

        if(contenedor.ataque1 == true)
        {
            contenedor.currentTimeAtaque1 -= Time.deltaTime;

            if (contenedor.currentTimeAtaque1 <= 0)
            {
               
                animator.SetBool("Recuperacion2seg", true);
                contenedor.currentTimeAtaque1 = contenedor.startingTimeAtaque1;

                contenedor.ataque1 = false;

            }

        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Ataque1", false);
        contenedor.anim.GetComponent<Animator>().SetBool("Combo3", false);
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
