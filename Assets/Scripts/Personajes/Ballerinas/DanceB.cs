using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DanceB : StateMachineBehaviour
{
    NavMeshAgent ballerina;
    public Transform[] dancePoints;
    public int nextPosition;
    public int maxPosition;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ballerina = animator.gameObject.GetComponent<NavMeshAgent>();
        for (int i = 0; i < dancePoints.Length; i++)
        {
            dancePoints[i] = animator.gameObject.GetComponent<Ballerinas>().dancePoints[i];
        }
            maxPosition = dancePoints.Length - 1;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ballerina.destination = dancePoints[nextPosition].position;
        if (Vector3.Distance(ballerina.transform.position, dancePoints[nextPosition].position) <= 2f)
        {
            if (nextPosition >= maxPosition)
            {
                nextPosition=0;
            }
            else
            {
                nextPosition++;
            }
            ballerina.destination = dancePoints[nextPosition].position;
        }
    }
}
