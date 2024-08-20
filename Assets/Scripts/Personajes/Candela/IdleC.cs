using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleC : StateMachineBehaviour
{
    public int choice;
    public Candela candelaBallerinas;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        candelaBallerinas = animator.GetComponent<Candela>();

        candelaBallerinas.StartBallerinas();
        choice = Random.Range(0, 10);
        if (choice <= 5)
        {
            animator.GetComponent<Animator>().SetBool("Attack1", true);
        }
        else
        {
            animator.GetComponent<Animator>().SetBool("Attack2", true);
        }
    }
}
