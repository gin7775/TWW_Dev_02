using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PerseguirStateFinalBoss : StateMachineBehaviour
{

    ContenedorFinalBoss contenedor;
    NavMeshAgent agent;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        
        contenedor = animator.gameObject.GetComponent<ContenedorFinalBoss>();

        contenedor.player = GameObject.FindGameObjectWithTag("Player");
        contenedor.currentTimeAttacks = contenedor.startingTimeAttacks;

        contenedor.anim.SetBool("Walk", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = 2.5f;
        agent.destination = contenedor.player.transform.position;

        contenedor.currentTimeAttacks -= 1 * Time.deltaTime; 

        if(contenedor.currentTimeAttacks <= 0)
        {
          int choiseAttack =  Random.Range(0, 3);

            if(choiseAttack == 0)
            {

                animator.SetBool("GolpeEspejismo", true);
                animator.SetBool("Perseguir", false);
            }

            if(choiseAttack == 1 )
            {
                animator.SetBool("AtaqueTornado", true);
                animator.SetBool("Perseguir", false);

            }
            if (choiseAttack == 2 || choiseAttack == 3)
            {
                animator.SetBool("Ataque1", true);
                animator.SetBool("Perseguir", false);

            }
            contenedor.currentTimeAttacks = 3f;

        }


        contenedor.gameObjectSon.transform.position = new Vector3(contenedor.gameObjectSon.transform.position.x, 0, contenedor.gameObjectSon.transform.position.z);
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedor.anim.SetBool("Walk", false);
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
