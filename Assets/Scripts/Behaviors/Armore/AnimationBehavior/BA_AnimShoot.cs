using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_AnimShoot : StateMachineBehaviour
{
    public float shootTimeUpdate = 4;
    public float shootTimer = 4;
    public ArmoreBoss bossArmore;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossArmore = animator.gameObject.GetComponentInParent<ArmoreBoss>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shootTimer = shootTimeUpdate;
        bossArmore.ArmoreHitme();
    }
}
