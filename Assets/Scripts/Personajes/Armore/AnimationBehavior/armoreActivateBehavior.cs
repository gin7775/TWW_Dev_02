using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armoreActivateBehavior : StateMachineBehaviour
{

    private Armore armore;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore = animator.gameObject.GetComponentInParent<Armore>();
        armore.ArmoreAction();
    }
   

}
