using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitted : StateMachineBehaviour
{
    public int recivedDamage;
    public int hits;
    public Candela candela;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hits++;
        candela = animator.GetComponent<Candela>();
        candela.animations.SetTrigger("Hitted");
        if (hits >= 3)
        {
            candela.animations.SetTrigger("Projectiles");
            animator.GetComponent<Animator>().SetBool("Proyectiles", true);
            hits = 0;
            candela.TakeDamage(recivedDamage);
        }
        if (candela.currentHealth <= 0)
        {
            animator.GetComponent<Animator>().SetBool("Phase2", true);
        }
    }
}
