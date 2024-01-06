using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SaltarScript : StateMachineBehaviour
{
    private Transform playerTransform;
    private float timer;
    private bool hasJumped;
    private Vector3 jumpTarget;
    ContainerTarik container;
    private NavMeshAgent agent;
    private Rigidbody rb;
   
    // Al entrar en el estado
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        container = animator.GetComponent<ContainerTarik>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        rb = animator.GetComponent<Rigidbody>();
        
        timer = 0f;
        hasJumped = false;

        // Elegir un punto de salto aleatorio
        if (container.jumpPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, container.jumpPoints.Count);
            jumpTarget = container.jumpPoints[randomIndex].position;
        }
    }

    // Actualización del estado
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Vector3.Distance(animator.transform.position, playerTransform.position) > 10f)
        {
            animator.SetTrigger("Patrullaje");
            container.animatorTarik.SetTrigger("Patrol");// Cambiar al estado de patrol
            return; // Salir del método para evitar ejecutar el código restante
        }

        if (!hasJumped && timer >= container.jumpDelay)
        {
            container.animatorTarik.SetTrigger("Jump");
            Jump(animator.transform);
            hasJumped = true;
        }

        // Verificar si el enemigo ha aterrizado
        if (Vector3.Distance(animator.transform.position, jumpTarget) < 1.0f) // Ajusta la distancia según sea necesario
        {
            animator.SetTrigger("Ataque"); // Cambiar al estado de ataque
        }

        timer += Time.deltaTime;
    }
  

    private void Jump(Transform enemyTransform)
    {
        if (agent != null)
        {
            agent.enabled = false;
        }

        if (rb != null)
        {
             
            container.StartCoroutine(container.ParabolicJump(enemyTransform, jumpTarget));
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        agent.enabled = true;

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
