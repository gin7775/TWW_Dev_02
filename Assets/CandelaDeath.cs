using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandelaDeath : StateMachineBehaviour
{
    public Candela candela;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        candela = animator.GetComponent<Candela>();
        candela.DeathBallerinas();
        candela.animations.SetTrigger("Death");
    }
}
