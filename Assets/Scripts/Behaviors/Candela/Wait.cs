using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wait : StateMachineBehaviour
{
    public Candela candelaBallerina;
    NavMeshAgent candela;
    public float time;
    public Candela animation;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        candela = animator.gameObject.GetComponent<NavMeshAgent>();
        candelaBallerina = animator.GetComponent<Candela>();
        animation = animator.GetComponent<Candela>();
        animation.animations.SetBool("Dance", false);
        animation.animations.SetBool("Idle", true);
        candelaBallerina.BallerinaDance();
        candela.speed = 0;
        time = Random.Range(10, 20);

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            animator.GetComponent<Animator>().SetBool("Attack1", false);
            animator.GetComponent<Animator>().SetBool("Attack2", true);
        }
    }
}
