using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armoreBlockBehavior : StateMachineBehaviour
{
    public float blockTimer=4;
    private Armore armore;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore = animator.gameObject.GetComponent<Armore>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore.guardState = 2;
        blockTimer -= Time.deltaTime;
        if (blockTimer <= 0)
        {
            animator.SetTrigger("HorizontalAtack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore.guardState = 1;
        blockTimer = 4;
    }


}
