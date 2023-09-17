using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAnimBehavior : StateMachineBehaviour
{

    public GameObject Holder_2,player;
    private Armore armore;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore = animator.gameObject.GetComponentInParent<Armore>();
        Holder_2 = armore.Holder_2;
        player = GameObject.FindGameObjectWithTag("Player");
    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject toInstanciate = Instantiate(Holder_2, new Vector3(animator.transform.position.x, animator.transform.position.y+0.3f, animator.transform.position.z - 0.5f), Quaternion.identity);
        toInstanciate.GetComponent<Holder>().SetGun(1,0.1f,3,true);
        toInstanciate.transform.LookAt(player.transform.position);
        armore.ArmoreHitme();
    }

   
}
