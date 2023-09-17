using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAnimBehavior : StateMachineBehaviour
{
    
    public GameObject Holder_1;
    private Armore armore;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore = animator.gameObject.GetComponentInParent<Armore>();
        Holder_1 = armore.Holder_1;
    }

   

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {       
        GameObject toInstanciate =  Instantiate(Holder_1,new Vector3( animator.transform.position.x, animator.transform.position.y + 1, animator.transform.position.z),Quaternion.identity);
        armore.ArmoreHitme();
    }

}
