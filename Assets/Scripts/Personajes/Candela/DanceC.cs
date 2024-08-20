using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DanceC : StateMachineBehaviour
{
    public Transform[] spinPoints;
    NavMeshAgent candela;
    public int nextPosition;
    public int maxPosition;
    public Candela candelaBallerinas;
    public float time;
    public Candela animation;
    public GameObject fire;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        candelaBallerinas = animator.GetComponent<Candela>();
        animation = animator.GetComponent<Candela>();
        candela = animator.gameObject.GetComponent<NavMeshAgent>();
        fire = animator.gameObject.GetComponent<Candela>().proyectile;
        animation.animations.SetBool("Dance", true);
        animation.animations.SetBool("Idle", false);
        for (int i = 0; i < spinPoints.Length; i++)
        {
            spinPoints[i] = animator.gameObject.GetComponent<Candela>().spinPoints[i];
        }
        maxPosition = spinPoints.Length - 1;
        candelaBallerinas.BallerinaLeave();
        candela.speed = 100;
        time = Random.Range(30, 60);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time -= Time.deltaTime;
        candela.destination = spinPoints[nextPosition].position;
        if (Vector3.Distance(candela.transform.position, spinPoints[nextPosition].position) <= 2f)
        {
            GameObject proyectile2 = Instantiate(fire, animator.transform.position, Quaternion.identity);
            if (nextPosition >= maxPosition)
            {
                nextPosition = 0;
            }
            else
            {
                nextPosition++;
            }
            candela.destination = spinPoints[nextPosition].position;
        }
        if (time <= 0)
        {
            candela.destination = spinPoints[9].position;
            candelaBallerinas.BallerinaReturn();
            animator.GetComponent<Animator>().SetBool("Attack2", false);
            animator.GetComponent<Animator>().SetBool("Attack1", true);
        }
    }
}
