using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armoreDeathBehavior : StateMachineBehaviour
{
    public ArmoreBoss bossArmore;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossArmore = animator.gameObject.GetComponentInParent<ArmoreBoss>();
        bossArmore.EndFace();
        Destroy(animator.gameObject, 6);
    }
    
}
