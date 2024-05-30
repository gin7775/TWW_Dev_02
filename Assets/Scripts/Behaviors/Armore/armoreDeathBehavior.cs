using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armoreDeathBehavior : StateMachineBehaviour
{
    public ArmoreBoss bossArmore;
    public Animator trapRelease;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossArmore = animator.gameObject.GetComponentInParent<ArmoreBoss>();
        trapRelease = bossArmore.trapToRelease;
        bossArmore.EndFace();
        trapRelease.SetTrigger("Active");
        Destroy(animator.gameObject, 6);
    }
    
}
