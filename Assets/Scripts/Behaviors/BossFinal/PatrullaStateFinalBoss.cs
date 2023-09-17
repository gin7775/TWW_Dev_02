using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrullaStateFinalBoss : StateMachineBehaviour
{
    ContenedorFinalBoss contenedor;
    NavMeshAgent agent;
    Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();

        contenedor = animator.gameObject.GetComponent<ContenedorFinalBoss>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = 1f;
        agent.destination = contenedor.wayPoints[contenedor.nextWayPoint].position;                                               
                                                                                    

        Vector3 difPosition = contenedor.wayPoints[contenedor.nextWayPoint].transform.position - animator.gameObject.transform.position;       //Calcula la diferencia de posición entre nuestro agente y el siguiente waypoint.
        //Debug.Log(difPosition);
        if (Mathf.Abs(difPosition.x) < 0.1f && Mathf.Abs(difPosition.z) < 0.1f)                                                           //Si nuestro vector3 en posición en x y z es menor a 0.1f.    
        {
            if (contenedor.nextWayPoint < contenedor.wayPoints.Length - 1)                                                              //Si nuestro siguiente waypoint es menor al tamaño de nuestro array menos uno.
            {
                contenedor.nextWayPoint++;                                                                                             //Se le suma uno al array, para que vaya a la siguiente posición.
            }
            else if (contenedor.nextWayPoint == contenedor.wayPoints.Length - 1)                                                         //Y si nuestro siguiente waypoint es igual al tamaño del array menos uno.
            {
                contenedor.nextWayPoint = 0;                                                                                             //El número del siguiente array es 0, esto es para crear una ruta cícicla y que nunca pare.
            }
        }


       if(agent.remainingDistance < 3)
        {
            animator.SetBool("Perseguir",true);
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
