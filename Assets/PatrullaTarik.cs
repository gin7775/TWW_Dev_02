using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrullaTarik : StateMachineBehaviour
{
    ContainerTarik container;
    NavMeshAgent agent;
    Transform playerTransform;
    float detectionRange = 10.0f;

    // Al entrar en el estado
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        container = animator.GetComponent<ContainerTarik>();
        agent = animator.GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        container.currentWaypointIndex = 0;
        if (container.waypoints.Count > 0)
        {
            agent.SetDestination(container.waypoints[container.currentWaypointIndex].position);
        }
    }

    // Actualización del estado
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (container.waypoints.Count == 0)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            container.currentWaypointIndex = (container.currentWaypointIndex + 1) % container.waypoints.Count;
            agent.SetDestination(container.waypoints[container.currentWaypointIndex].position);
        }

        // Comprobar la distancia al jugador
        if (Vector3.Distance(animator.transform.position, playerTransform.position) <= detectionRange)
        {
            // Activar el trigger para cambiar al estado de ataque
            animator.SetTrigger("Ataque");
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
}
