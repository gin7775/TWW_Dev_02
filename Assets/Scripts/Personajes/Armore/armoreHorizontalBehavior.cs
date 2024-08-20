using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class armoreHorizontalBehavior : StateMachineBehaviour
{
    public float returnTimer = 5;
    private Armore armore;
    public GameObject player;
    private NavMeshAgent armoreAgent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armoreAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        armore = animator.gameObject.GetComponent<Armore>();
        player = armore.player;
        armore.ArmoreHorizontal();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player.transform.position);
        returnTimer -= Time.deltaTime;
        if (returnTimer <= 0)
        {
            animator.SetTrigger("HitMe");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        returnTimer = 5;
    }

 
}
