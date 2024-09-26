using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

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
        // Si no est� atacando, el agente se mueve normalmente
        if (!isDashing)
        {
            puppet.speed = 2f;
            puppet.destination = destination.position;

            // Si el enemigo est� a la distancia de ataque (1 metro)
            if (Vector3.Distance(puppet.transform.position, destination.position) <= stopDistance)
            {
                puppet.isStopped = true; // Detener el agente de navegaci�n
                puppet.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(PauseAndAttack(animator));
            }
        }
    }

    // Corrutina para detenerse y luego hacer el desplazamiento de ataque
    private IEnumerator PauseAndAttack(Animator animator)
    {
        float elapsedTime = 0f;

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

        // Desactivar temporalmente el NavMeshAgent para evitar conflictos con DOTween
        puppet.enabled = false;

        // Iniciar el dash hacia la posici�n calculada
        isDashing = true;
        puppet.transform.DOMove(dashTargetPosition, dashDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Reactivar el NavMeshAgent despu�s del dash
            puppet.enabled = true;

            // Actualizar la posici�n del NavMeshAgent para que est� sincronizado
            puppet.Warp(dashTargetPosition);

            // Transicionar al estado de ataque o continuar con el comportamiento
            animator.SetTrigger("Attack");
            contenedorPuppet.animPuppet.SetBool("Attack", false);
            puppet.speed = 2f;
            isDashing = false;
        });
    }

    // OnStateExit es llamado cuando el estado termina
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedorPuppet.animPuppet.SetBool("Walk", false);
        isDashing = false; // Resetear el ataque si sale del estado
    }
}
