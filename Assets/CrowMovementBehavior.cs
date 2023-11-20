using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMovementBehavior : StateMachineBehaviour
{
    public CrowMovement crow;
    public GameObject crowSkull, player;
    public Animator animation;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        crow = animator.gameObject.GetComponent<CrowMovement>();
        crowSkull = crow.crowSkull;
        player = crow.player;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        crow.MoveTowards();
        if (Vector3.Distance(crow.transform.position, player.transform.position) <= 1f)
        {
           // animation.SetTrigger("Shoot");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
