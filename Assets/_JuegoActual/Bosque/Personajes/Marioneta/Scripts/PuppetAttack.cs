using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuppetAttack : StateMachineBehaviour
{
    NavMeshAgent puppet;
    public Transform destination;
  
    ContenedorPuppet contenedorPuppet;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puppet = animator.gameObject.GetComponent<NavMeshAgent>();
        destination = animator.gameObject.GetComponent<Puppet>().player;
        contenedorPuppet = animator.gameObject.GetComponent<ContenedorPuppet>();

        

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puppet.speed = 2f;

        puppet.destination = destination.position;

        contenedorPuppet.waitingTimeAttacking -= Time.deltaTime;

      




        //Realiza la animación de atacar                    
        if (contenedorPuppet.waitingTimeAttacking <= 0)
        {
            animator.SetTrigger("Hitme");
            contenedorPuppet.animPuppet.SetBool("Attack", false);

            contenedorPuppet.animPuppet.SetBool("Idle", true);



        }



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //if (contenedorPuppet.projectilesLock == true)
        //{
        //    Instantiate(contenedorPuppet.projectiles, animator.transform.position, Quaternion.identity);

        //    contenedorPuppet.projectilesLock = false;
        //}

        contenedorPuppet.waitingTimeAttacking = 3f;
    }

}
