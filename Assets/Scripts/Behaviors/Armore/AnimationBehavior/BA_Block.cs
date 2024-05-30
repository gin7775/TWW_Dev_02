using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BA_Block : StateMachineBehaviour
{
    public float blockTimer = 4;
    private ArmoreBoss armore;
    private NavMeshAgent BA_Agent;
    [SerializeField]
    private float speedStart;
    public GameObject player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore = animator.gameObject.GetComponent<ArmoreBoss>();
        BA_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        armore.AnimArmoreBlock();
        speedStart = BA_Agent.speed;
        BA_Agent.speed = 0;
        player = armore.player;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player.transform.position);
        armore.guardState = 2;
        blockTimer -= Time.deltaTime;
        if (blockTimer <= 0)
        {
            armore.AnimArmoreShieldRelease();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armore.guardState = 1;
        BA_Agent.speed = 4;
        blockTimer = 4;
    }

}
