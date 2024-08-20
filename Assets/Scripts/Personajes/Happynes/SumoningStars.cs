using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoningStars : StateMachineBehaviour
{
    public Transform axisPos;
    public Vector3 currentPos;
    public Transform[] spawnPos;
    public float spawnTimer,maxTimer;
    public HappyParamet parametros;
    //public GameObject []currentServants;
    GameObject[] servantArmores;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        axisPos = animator.GetBehaviour<HappyActionBehavior>().axisRoot;
        parametros = animator.GetBehaviour<HappyActionBehavior>().parametros;
        servantArmores = parametros.armoreServant;
        spawnPos = parametros.parametSpawnPos;
        currentPos = animator.transform.position;
        animator.transform.position = axisPos.position;
        servantSpawner(servantArmores);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            animator.SetTrigger("Hitme");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spawnTimer = maxTimer;
        animator.transform.position = currentPos;
        for (int i= 0; i <= servantArmores.Length;i++)
        {
            Destroy(servantArmores[i]);

        }
        parametros.armoreServant = GameObject.FindGameObjectsWithTag("Armore");
        servantArmores = parametros.armoreServant;
    }

    public void servantSpawner( GameObject [] toInstanciate )
    {
        GameObject servants;
        //GameObject [] servants;
        for(int i = 0; i < toInstanciate.Length; i++)
        {
            
            GameObject instance = GameObject.Instantiate(toInstanciate[i], spawnPos[i].position, Quaternion.identity);
            servants = instance;
            servants.GetComponent<Armore>().ArmoreActivate();
            servantArmores[i] = servants;
        }       
        
    }

}

    
