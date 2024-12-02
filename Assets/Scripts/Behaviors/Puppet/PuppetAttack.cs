using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PuppetAttack : StateMachineBehaviour
{
    NavMeshAgent puppet;
    public Transform destination;
    ContenedorPuppet contenedorPuppet;

    private float waitAfterAttackDuration = 1f; // Tiempo de espera en este estado

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puppet = animator.gameObject.GetComponent<NavMeshAgent>();
        destination = animator.gameObject.GetComponent<Puppet>().player;
        contenedorPuppet = animator.gameObject.GetComponent<ContenedorPuppet>();

        if (puppet.enabled && puppet.isOnNavMesh)
        {
            puppet.isStopped = true; // Detener el movimiento del agente durante el ataque
        }
        // Iniciar la corrutina para esperar 1 segundo antes de pasar a navegaciÛn
        puppet.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(WaitBeforeNavigation(animator));
    }

    // OnStateUpdate es llamado cada frame mientras estÅEen este estado
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Asegurarse de que el enemigo estÅEsiempre mirando al jugador
        if (destination != null)
        {
            Vector3 directionToPlayer = (destination.position - puppet.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); // Ignorar la rotaciÛn en el eje Y
            puppet.transform.rotation = Quaternion.Slerp(puppet.transform.rotation, lookRotation, Time.deltaTime * 5f); // Ajustar la velocidad de rotaciÛn
        }
    }

    // Corrutina para esperar 1 segundo antes de volver a la navegaciÛn
    private IEnumerator WaitBeforeNavigation(Animator animator)
    {
        // Pausa de 1 segundo en este estado
        yield return new WaitForSeconds(waitAfterAttackDuration);

        // Volver al estado de navegaciÛn
        animator.SetTrigger("Navigation");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (puppet.enabled && puppet.isOnNavMesh)
        {
            puppet.isStopped = false; // Reanudar el movimiento del agente
        }


        // Reiniciar el tiempo de ataque si es necesario
        contenedorPuppet.waitingTimeAttacking = 3f;
    }
}
