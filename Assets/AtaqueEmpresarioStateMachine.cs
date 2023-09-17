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
        contenedorEmpresario.cooldownTime3 = contenedorEmpresario.coolDown3;
        contenedorEmpresario.cooldownTime2 = contenedorEmpresario.coolDown2;
        contenedorEmpresario.animEmpresario.SetBool("Andar", false);
        contenedorEmpresario.animEmpresario.SetTrigger("ataque");
        contenedorEmpresario.projectilesLock = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        empresario.speed = 0f;
        
        contenedorEmpresario.coolDown -= Time.deltaTime;
        contenedorEmpresario.coolDown3 -= Time.deltaTime;
        

        if (contenedorEmpresario.coolDown3 <= 0)
        {
            contenedorEmpresario.coolDownAnim = true;
            contenedorEmpresario.animEmpresario.SetTrigger("ataque");
            contenedorEmpresario.coolDown3 = contenedorEmpresario.cooldownTime3;
        
        }

        if(contenedorEmpresario.coolDownAnim == true)
        {

            contenedorEmpresario.coolDown2 -= Time.deltaTime;

            if (contenedorEmpresario.coolDown2 <= 0)
            {

                contenedorEmpresario.animEmpresario.SetTrigger("descanso");
                contenedorEmpresario.coolDown2 = contenedorEmpresario.cooldownTime2;
                contenedorEmpresario.coolDownAnim = false;
            }
          
        }

        //animator.transform.LookAt(player);
        if (contenedorEmpresario.coolDown <= 0)
        {
            contenedorEmpresario.projectilesLock = true;
            contenedorEmpresario.coolDown = 2f;

        }
        animator.transform.LookAt(player.transform.position);
        
        if (contenedorEmpresario.projectilesLock == true)
        {
            Instantiate(contenedorEmpresario.projectiles, contenedorEmpresario.spawnProyectile.position, Quaternion.identity);
            

            contenedorEmpresario.projectilesLock = false;
        }

        if (Vector3.Distance(empresario.transform.position, player.position) >= 13f)
        {
            animator.SetTrigger("Patrol");

        }

        if (Vector3.Distance(empresario.transform.position, player.position) <= 3f)
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
