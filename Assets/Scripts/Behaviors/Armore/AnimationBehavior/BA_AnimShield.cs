using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_AnimShield : StateMachineBehaviour
{
    public float guardTimeUpdate = 3;
    public float guardTimer = 3;
    public ArmoreBoss bossArmore;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossArmore = animator.gameObject.GetComponentInParent<ArmoreBoss>();
        //bossArmore.guardState = 2;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //bossArmore.guardState = 2;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // bossArmore.guardState = 3;
        //guardTimer = guardTimeUpdate;
    }

}
