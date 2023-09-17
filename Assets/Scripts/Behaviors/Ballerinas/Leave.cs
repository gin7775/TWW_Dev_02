using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Leave : StateMachineBehaviour
{
    public Transform pointLeave;
    NavMeshAgent ballerina;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ballerina = animator.gameObject.GetComponent<NavMeshAgent>();
        pointLeave = animator.gameObject.GetComponent<Ballerinas>().exitPoint;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ballerina.speed = 10;
        ballerina.destination = pointLeave.position;
    }
}
