using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyActionBehavior : StateMachineBehaviour
{

    public HappyParamet parametros;
    public Transform axisRoot;
    public int choiceIterator,randomIterator;
    public bool oneStar;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parametros = animator.gameObject.GetComponent<HappyParamet>();
        axisRoot = parametros.axisRoot;

        randomIterator = Random.Range(1, 3);
        if (randomIterator == 1)
        {
            choiceIterator = 1;

        }
        else
        {
            choiceIterator = 2;
        }
        if (parametros.currentHealth <= parametros.maxHealth / 2 && oneStar == true)
        {
            oneStar = false;
            choiceIterator = 3;

        }
        if (choiceIterator == 2)
        {
            animator.SetTrigger("RotatingStars");
        }
        else if (choiceIterator == 3)
        {
            animator.SetTrigger("StarDancing");
        }
        else
        {
            animator.SetTrigger("SumoningStars");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        randomIterator = Random.Range(1,3);
        if(randomIterator == 1)
        {
            choiceIterator = 1;

        }
        else
        {
            choiceIterator = 2;
        }
        if (parametros.currentHealth <= parametros.maxHealth/2 && oneStar == true)
        {
            oneStar = false;
            choiceIterator = 3;

        }
        if(choiceIterator == 2)
        {
            animator.SetTrigger("RotatingStars");
        }
        else if (choiceIterator == 3)
        {
            animator.SetTrigger("StarDancing");
        }
        else
        {
            animator.SetTrigger("SumoningStars");
        }*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
