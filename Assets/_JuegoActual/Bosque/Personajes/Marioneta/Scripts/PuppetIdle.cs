using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuppetIdle : StateMachineBehaviour
{
    public Transform player;
    ContenedorPuppet contenedorPuppet;
    NavMeshAgent puppet;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puppet = animator.gameObject.GetComponent<NavMeshAgent>();
        player = animator.gameObject.GetComponent<Puppet>().player;
        contenedorPuppet = animator.gameObject.GetComponent<ContenedorPuppet>();
        contenedorPuppet.maxPosition = contenedorPuppet.destination.Length - 1;

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedorPuppet.animPuppet.SetBool("Walk", true);
        puppet.speed = 2f;
        puppet.destination = contenedorPuppet.destination[contenedorPuppet.nextPosition].position;
        if (Vector3.Distance(puppet.transform.position, contenedorPuppet.destination[contenedorPuppet.nextPosition].position) <= 2f)
        {
            if (contenedorPuppet.nextPosition >= contenedorPuppet.maxPosition)
            {
                contenedorPuppet.nextPosition = 0;
            }
            else
            {
                contenedorPuppet.nextPosition++;
            }
            puppet.destination = contenedorPuppet.destination[contenedorPuppet.nextPosition].position;
        }

        
        if (Vector3.Distance(puppet.transform.position, player.position) <= 4f)
        {
            animator.SetTrigger("Navigation");

        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
       
    }
}
