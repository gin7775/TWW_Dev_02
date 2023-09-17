using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTornado : MonoBehaviour
{
    NavMeshAgent bossFinal;
    private GameObject[] points;
    public int nextPosition;
    public int maxPosition;
    void Start()
    {


        points = GameObject.FindGameObjectsWithTag("Points");
        bossFinal = this.gameObject.GetComponent<NavMeshAgent>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        bossFinal.destination = points[nextPosition].transform.position;
        if (Vector3.Distance(bossFinal.transform.position, points[nextPosition].transform.position) <= 2f)
        {
            if (nextPosition >= maxPosition)
            {
                nextPosition = 0;
            }
            else
            {
                nextPosition++;
            }
            bossFinal.destination = points[nextPosition].transform.position;
        }

        Destroy(this.gameObject, 20);
    }
}
