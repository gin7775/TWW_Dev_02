using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_Block : StateMachineBehaviour
{
    public float blockTimer = 4;
    private ArmoreBoss armore;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore = animator.gameObject.GetComponent<ArmoreBoss>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore.guardState = 2;
        blockTimer -= Time.deltaTime;
        if (blockTimer <= 0)
        {
            armore.AnimArmoreShieldRelease();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore.guardState = 1;
        blockTimer = 4;
    }

}
