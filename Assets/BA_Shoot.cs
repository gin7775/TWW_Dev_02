using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_Shoot : StateMachineBehaviour
{
    public float returnTimer = 5;
    private ArmoreBoss armore;
    public GameObject player;
    private UnityEngine.AI.NavMeshAgent armoreAgent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armoreAgent = animator.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        armore = animator.gameObject.GetComponent<ArmoreBoss>();
        player = armore.player;
        armore.AnimArmoreShoot();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player.transform.position);
        returnTimer -= Time.deltaTime;
        if (returnTimer <= 0)
        {
            animator.SetTrigger("Action");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        returnTimer = 5;
    }
}
