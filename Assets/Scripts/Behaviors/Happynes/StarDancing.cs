using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDancing : StateMachineBehaviour
{
    public HappyParamet parametros;
    public float danceTimer = 16;
    public Transform axisRoot;
    public GameObject proyectileHolder,toInstanciate;
   

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        parametros = animator.gameObject.GetComponent<HappyParamet>();
        proyectileHolder = parametros.proyectileHolders[0];
        axisRoot = parametros.axisRoot;
        toInstanciate = GameObject.Instantiate(proyectileHolder, axisRoot.position,Quaternion.identity);
        toInstanciate.GetComponent<Holder>().SetGun(15, 0.4f, 3, false);
        toInstanciate.transform.LookAt(axisRoot.position);
       toInstanciate.transform.Rotate(new Vector3(0,1,0), -180f); 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        danceTimer -= Time.deltaTime;
        if (danceTimer <= 0)
        {
            animator.SetTrigger("Action");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
