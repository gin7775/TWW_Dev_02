using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSustitute : MonoBehaviour
{
    public Transform[] destination;
    public int nextPosition;
    public int maxPosition;
    NavMeshAgent a;
    void Start()
    {
        a = this.gameObject.GetComponent<NavMeshAgent>();
        maxPosition = destination.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        a.destination = destination[nextPosition].position;
        if (Vector3.Distance(a.transform.position, destination[nextPosition].position) <= 2f)
        {
            if (nextPosition >= maxPosition)
            {
                nextPosition = 0;
            }
            else
            {
                nextPosition++;
            }
            a.destination = destination[nextPosition].position;
        }
    }
}
