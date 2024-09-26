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

        // Detener el NavMeshAgent inmediatamente al entrar en el estado
        puppet.isStopped = true;

        // Iniciar la corrutina para esperar 1 segundo antes de pasar a navegación
        puppet.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(WaitBeforeNavigation(animator));
    }

    // OnStateUpdate es llamado cada frame mientras está en este estado
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Asegurarse de que el enemigo esté siempre mirando al jugador
        if (destination != null)
        {
            Vector3 directionToPlayer = (destination.position - puppet.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); // Ignorar la rotación en el eje Y
            puppet.transform.rotation = Quaternion.Slerp(puppet.transform.rotation, lookRotation, Time.deltaTime * 5f); // Ajustar la velocidad de rotación
        }
    }

    // Corrutina para esperar 1 segundo antes de volver a la navegación
    private IEnumerator WaitBeforeNavigation(Animator animator)
    {
        // Pausa de 1 segundo en este estado
        yield return new WaitForSeconds(waitAfterAttackDuration);

        // Volver al estado de navegación
        animator.SetTrigger("Navigation");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reactivar el NavMeshAgent cuando salga del estado
        puppet.isStopped = false;

        // Reiniciar el tiempo de ataque si es necesario
        contenedorPuppet.waitingTimeAttacking = 3f;
    }
}
