using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleB : StateMachineBehaviour
{
    public bool dance;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dance = animator.gameObject.GetComponent<Ballerinas>().dance;
        if (dance)
        {
            animator.GetComponent<Animator>().SetBool("dance", true);
            dance = false;
            animator.gameObject.GetComponent<Ballerinas>().dance = dance;
            animator.GetComponent<Animator>().SetBool("stop", false);
        }
    }
}
