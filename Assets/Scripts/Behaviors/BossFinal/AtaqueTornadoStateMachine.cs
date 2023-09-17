using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtaqueTornadoStateMachine : StateMachineBehaviour
{

    ContenedorFinalBoss contenedor;
    NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        agent = animator.gameObject.GetComponent<NavMeshAgent>();

        contenedor = animator.gameObject.GetComponent<ContenedorFinalBoss>();
        contenedor.currentTimeGolpeTornado = contenedor.startingTimeGolpeTornado;

        contenedor.anim.SetBool("Tornado", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedor.currentTimeGolpeTornado -= Time.deltaTime;
        agent.speed = 0f;
        animator.transform.LookAt(contenedor.transform.position);



        if (contenedor.currentTimeGolpeTornado <= 0)
        {

            animator.SetBool("Perseguir", true);
            animator.SetBool("AtaqueTornado", false);
            


        }


    }

    void InstantiateProjectile()
    {
        var projectileObj = Instantiate(contenedor.tornado[0], contenedor.TornadoPoints[0].position, Quaternion.identity);
        var projectileObj1 = Instantiate(contenedor.tornado[1], contenedor.TornadoPoints[1].position, Quaternion.identity);
        var projectileObj2 = Instantiate(contenedor.tornado[2], contenedor.TornadoPoints[2].position, Quaternion.identity);   

    }

   

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedor.anim.SetBool("Tornado", false);
        InstantiateProjectile();
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
