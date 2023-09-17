using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Wait : StateMachineBehaviour
{
    public float timeToAct;
    public int choiceIterator;
    [SerializeField]
    public Vector3 rotationSpeed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timeToAct = Random.Range(1.5f, 5);
        choiceIterator = Random.Range(0, 2);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.Rotate(rotationSpeed * Time.deltaTime);

        timeToAct -= Time.deltaTime;
        if (timeToAct <= 0)
        {

            choiceIterator = Random.Range(0, 3);
           
            if (choiceIterator == 2)
            {
                animator.SetTrigger("Atack2");
            }
            else
            {
                animator.SetTrigger("Atack1");
            }

        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        choiceIterator = Random.Range(0, 2);
        timeToAct = Random.Range(1.5f, 4);
        rotationSpeed.y = Random.Range(20, 40);

    }


}
