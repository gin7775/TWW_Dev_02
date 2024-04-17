using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowChoice : StateMachineBehaviour
{
    public CrowMovement crow;
    public GameObject crowSkull, player;
    public Animator animation;
    public int choiceIterator;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        crow = animator.gameObject.GetComponent<CrowMovement>();
        crowSkull = crow.crowSkull;
        player = crow.player;
        animation = crow.animationCrow;
        animation.SetTrigger("Idle");
        choiceIterator = Random.Range(0, 4);
        if (choiceIterator > 0) 
        {

            animator.SetTrigger("Shoot");

        
        }
        else 
        {
            animator.SetTrigger("Moving");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
