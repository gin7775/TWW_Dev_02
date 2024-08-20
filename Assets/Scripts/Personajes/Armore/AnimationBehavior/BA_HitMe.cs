using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_HitMe : StateMachineBehaviour
{
    public float hitTimer;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitTimer -= Time.deltaTime;
        if (hitTimer <= 0)
        {
            animator.SetTrigger("Action");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitTimer = 1.5f;
    }

}
