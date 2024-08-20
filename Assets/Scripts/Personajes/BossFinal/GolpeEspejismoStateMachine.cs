using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolpeEspejismoStateMachine : StateMachineBehaviour
{

    ContenedorFinalBoss contenedor;
    NavMeshAgent agent;
   

    private Vector3 destination;

    

    private GroundSlash groundSlashScript;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        agent = animator.gameObject.GetComponent<NavMeshAgent>();

        contenedor = animator.gameObject.GetComponent<ContenedorFinalBoss>();
        contenedor.currentTimeGolpeEspejismo = contenedor.startingTimeGolpeEspejismo;

        contenedor.anim.SetBool("Espejismo", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedor.currentTimeGolpeEspejismo -= Time.deltaTime;
        agent.speed = 0f;
        animator.transform.LookAt(contenedor.transform.position);

      

        if (contenedor.currentTimeGolpeEspejismo <= 0)
        {

            animator.SetBool("Perseguir", true);
            animator.SetBool("GolpeEspejismo", false);
            InstantiateProjectile();


        }
        

        

    }

    void InstantiateProjectile()
    {
        var projectileObj = Instantiate(contenedor.SlashPrefab[0],contenedor.fireGroundSlash.position , Quaternion.identity) as GameObject;
        var projectileObj1 = Instantiate(contenedor.SlashPrefab[1], contenedor.fireGroundSlash.position, Quaternion.identity) as GameObject;
        var projectileObj2 = Instantiate(contenedor.SlashPrefab[2], contenedor.fireGroundSlash.position, Quaternion.identity) as GameObject;

        groundSlashScript = projectileObj.GetComponent<GroundSlash>();

        RotateToDestination(projectileObj, destination, true);

        projectileObj.GetComponent<Rigidbody>().velocity = agent.transform.forward * groundSlashScript.speed;

        projectileObj1.GetComponent<Rigidbody>().velocity = agent.transform.forward * groundSlashScript.speed - agent.transform.right * groundSlashScript.speed;

        projectileObj2.GetComponent<Rigidbody>().velocity = agent.transform.forward * groundSlashScript.speed + agent.transform.right * groundSlashScript.speed;

    }

    void RotateToDestination(GameObject obj, Vector3 destination, bool onlyY)
    {

        var direction = destination - obj.transform.position;

        var rotation = Quaternion.LookRotation(direction);

        if (onlyY)
        {
            rotation.x = 0;
            rotation.z = 0;
        }

        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        contenedor.anim.SetBool("Espejismo", false);
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
