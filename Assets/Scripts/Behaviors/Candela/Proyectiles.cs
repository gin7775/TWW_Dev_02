using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectiles : StateMachineBehaviour
{
    public GameObject fire;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fire = animator.gameObject.GetComponent<Candela>().proyectile;
        GameObject proyectile2 = Instantiate(fire, animator.transform.position, Quaternion.identity);
        animator.GetComponent<Animator>().SetBool("Proyectiles", false);
        animator.GetComponent<Animator>().SetTrigger("Wait");
    }
}
