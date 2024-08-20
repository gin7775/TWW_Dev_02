using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Return : StateMachineBehaviour
{
    public Transform pointReturn;
    NavMeshAgent ballerina;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ballerina = animator.gameObject.GetComponent<NavMeshAgent>();
        pointReturn = animator.gameObject.GetComponent<Ballerinas>().startPoint;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ballerina.speed = 10;
        ballerina.destination = pointReturn.position;
        if (Vector3.Distance(ballerina.transform.position, pointReturn.position) <= 2f)
        {
            animator.SetBool("leave", false);
            animator.SetBool("return", false);
            animator.SetBool("stop", true);
        }
    }
}
