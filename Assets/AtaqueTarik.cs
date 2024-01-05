using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtaqueTarik : StateMachineBehaviour
{
    
        ContainerTarik container;
        Transform playerTransform;

         NavMeshAgent agent;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            container = animator.GetComponent<ContainerTarik>();
            
            agent = animator.GetComponent<NavMeshAgent>();
            agent.enabled = true;

        }

        
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        if (Vector3.Distance(animator.transform.position, playerTransform.position) <= container.jumpTriggerDistance)
        {
            // Activar el trigger para cambiar al estado de salto
            animator.SetTrigger("Saltar");
        }
        else if (Time.time >= container.nextAttackTime)
        {
            GameObject projectile = Instantiate(container.projectilePrefab, container.SpawnPosition.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = CalculateProjectileVelocity(animator.transform, playerTransform, container.initialVelocity, container.angle);
            container.nextAttackTime = Time.time + 1f / container.attackRate;
        }
    }

        Vector3 CalculateProjectileVelocity(Transform source, Transform target, float initialVelocity, float angle)
        {
            Vector3 direction = target.position - source.position; // Dirección del objetivo desde la fuente
            float yOffset = direction.y; // Diferencia de altura
            direction.y = 0; // Ignora la altura para el cálculo de la distancia horizontal
            float distance = direction.magnitude; // Distancia horizontal entre fuente y objetivo
            float radiansAngle = Mathf.Deg2Rad * angle; // Convertir ángulo a radianes

            direction.y = distance * Mathf.Tan(radiansAngle); // Establece la altura basada en el ángulo y la distancia
            distance += yOffset / Mathf.Tan(radiansAngle); // Ajusta la distancia horizontal para considerar la altura

            // Velocidad en cada eje
            float velocityX = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radiansAngle));
            return velocityX * direction.normalized;
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

