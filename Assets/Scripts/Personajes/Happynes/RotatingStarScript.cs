using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStarScript : StateMachineBehaviour
{

    public Transform axisRoot;
    public float rotationSpeed;
    public float rootTimer;
    public int dirIterator;
    public GameObject proyectileHolder, toInstanciate;
    public HappyParamet parametros;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        axisRoot = animator.GetBehaviour<HappyActionBehavior>().axisRoot;
        parametros = animator.GetBehaviour<HappyActionBehavior>().parametros;
        dirIterator = Random.Range(0, 2);
        proyectileHolder = parametros.proyectileHolders[1];
        toInstanciate = GameObject.Instantiate(proyectileHolder, animator.transform.position, Quaternion.identity);
        toInstanciate.GetComponent<Holder>().SetGun(12, 0.5f, 4, true);
        toInstanciate.transform.SetParent(animator.transform);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (dirIterator == 0)
        {
            animator.transform.RotateAround(axisRoot.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.transform.RotateAround(axisRoot.position, new Vector3(0, -1, 0), rotationSpeed * Time.deltaTime);
        }
        animator.transform.LookAt(axisRoot.position);
        rootTimer -= Time.deltaTime;
        if (rootTimer <= 0)
        {
            animator.SetTrigger("Hitme");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rootTimer = 7;
    }

   
}
