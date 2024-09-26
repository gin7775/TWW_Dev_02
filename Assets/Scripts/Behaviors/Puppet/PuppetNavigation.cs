using DG.Tweening;
using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class PuppetNavigation : StateMachineBehaviour
{
    public Transform destination;
    NavMeshAgent puppet;
    ContenedorPuppet contenedorPuppet;
    public float stopDistance = 1f; // Distancia para detenerse antes de atacar
    public float dashDistance = 5f; // Distancia fija del dash
    public float maxDashDistance = 7f; // Distancia m�xima permitida para el dash
    public float dashSpeed = 10f; // Velocidad del dash
    public float dashDuration = 0.3f; // Duraci�n del dash
    public float stopTimeBeforeAttack = 0.5f; // Tiempo de pausa antes del ataque

    private bool isDashing = false; // Controla si el enemigo est� haciendo el dash
    private Vector3 dashTargetPosition; // Posici�n hacia donde se har� el dash
    private Coroutine attackCoroutine; // Referencia a la corrutina para poder detenerla

    // OnStateEnter es llamado al entrar en este estado
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puppet = animator.gameObject.GetComponent<NavMeshAgent>();
        destination = animator.gameObject.GetComponent<Puppet>().player;
        contenedorPuppet = animator.gameObject.GetComponent<ContenedorPuppet>();
        contenedorPuppet.animPuppet.SetBool("Walk", true);
    }

    // OnStateUpdate es llamado en cada frame mientras est� en este estado
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Asegurarnos de que el NavMeshAgent est� activo y en una superficie NavMesh
        if (!isDashing && puppet.enabled && puppet.isOnNavMesh)
        {
            puppet.speed = 2f;
            puppet.destination = destination.position;

            // Si el enemigo est� a la distancia de ataque (1 metro)
            if (Vector3.Distance(puppet.transform.position, destination.position) <= stopDistance)
            {
                puppet.isStopped = true; // Detener el agente de navegaci�n
                if (attackCoroutine == null)
                {
                    attackCoroutine = puppet.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(PauseAndAttack(animator));
                }
            }
        }
    }

    // Corrutina para detenerse y luego hacer el desplazamiento de ataque
    private IEnumerator PauseAndAttack(Animator animator)
    {
        float elapsedTime = 0f;
        contenedorPuppet.animPuppet.SetTrigger("Attack");
        puppet.enabled = false;
        // Mientras transcurre el tiempo de espera, seguir mirando al jugador
        while (elapsedTime < stopTimeBeforeAttack)
        {
            // Calcular la direcci�n hacia el jugador
            Vector3 directionToPlayer = (destination.position - puppet.transform.position).normalized;

            // Rotar al enemigo para que mire hacia el jugador
            puppet.transform.rotation = Quaternion.LookRotation(directionToPlayer);

            // Incrementar el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Esperar al siguiente frame
            yield return null;
        }

        // Calcular la distancia al jugador
        float distanceToPlayer = Vector3.Distance(puppet.transform.position, destination.position);

        // Calcular la direcci�n hacia el jugador
        Vector3 directionToPlayerDash = (destination.position - puppet.transform.position).normalized;

        // Si la distancia al jugador es mayor que la distancia m�xima del dash, limitamos el dash
        if (distanceToPlayer > maxDashDistance)
        {
            dashTargetPosition = puppet.transform.position + directionToPlayerDash * maxDashDistance;
        }
        else
        {
            dashTargetPosition = puppet.transform.position + directionToPlayerDash * Mathf.Min(dashDistance, distanceToPlayer);
        }

        // Rotar al enemigo para que mire hacia el jugador justo antes del dash
        puppet.transform.rotation = Quaternion.LookRotation(directionToPlayerDash);

        // Iniciar el dash hacia la posici�n calculada
        isDashing = true;
        puppet.transform.DOMove(dashTargetPosition, dashDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Reactivar el NavMeshAgent despu�s del dash
            puppet.enabled = true;

            // Asegurarse de que el NavMeshAgent est� sobre el NavMesh antes de Warp
            if (puppet.isOnNavMesh)
            {
                // Actualizar la posici�n del NavMeshAgent para que est� sincronizado
                puppet.Warp(dashTargetPosition);
            }

            // Transicionar al estado de ataque o continuar con el comportamiento
            animator.SetTrigger("Attack");

            puppet.speed = 2f;
            isDashing = false;
            contenedorPuppet.animPuppet.SetBool("Walk", false);
            // Resetear la referencia de la corrutina
            attackCoroutine = null;
        });
    }

    // OnStateExit es llamado cuando el estado termina
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedorPuppet.animPuppet.SetBool("Walk", false);
        isDashing = false; // Resetear el ataque si sale del estado
        attackCoroutine = null; // Asegurarse de resetear la corrutina al salir
    }

    // M�todo para detener la corrutina desde otro lugar, como el knockback
    public void StopAttackCoroutine()
    {
        if (attackCoroutine != null)
        {
            puppet.gameObject.GetComponent<MonoBehaviour>().StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}
