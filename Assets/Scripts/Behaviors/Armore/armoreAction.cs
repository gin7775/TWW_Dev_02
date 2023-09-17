using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class armoreAction : StateMachineBehaviour
{
    private Armore armore;
    public int choiceIterator,ramdomIterator;
    public GameObject player;
    private NavMeshAgent armoreAgent;
    public bool healOnce = true; 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armoreAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        armore = animator.gameObject.GetComponent<Armore>();
        player = armore.player;
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armoreAgent.destination = player.transform.position;
        armore.ArmoreWalk(1);
        if (armoreAgent.remainingDistance <=2.1f )
        {
            if (armoreAgent.remainingDistance <= 1.3f)
            {
                ramdomIterator = Random.Range(1, 3);
                if (ramdomIterator == 1)
                {
                    choiceIterator = 1;
                }
                else if (ramdomIterator == 2)
                {
                    choiceIterator = 3;
                }

            }
            else
            {
                choiceIterator = Random.Range(1, 4);
            }

            if(armore.currentHealth <= 40 && healOnce == true)
            {
                choiceIterator = 4;
            }

            if (choiceIterator == 1)
            {
                animator.SetTrigger("VerticalAtack");
            }
            else if (choiceIterator == 2)
            {
                animator.SetTrigger("HorizontalAtack");
            }
            else if (choiceIterator == 3)
            {
                animator.SetTrigger("Block");
            }
            else if (choiceIterator == 4)
            {
                healOnce = false;
                animator.SetTrigger("Heal");
            }
        }
        else
        {
            if (armore.currentHealth <= 40 && healOnce == true)
            {
                choiceIterator = 4;
                healOnce = false;
                animator.SetTrigger("Heal");
            }
        }
    }

    
}
