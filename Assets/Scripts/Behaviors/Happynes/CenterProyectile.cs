using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterProyectile : MonoBehaviour
{
    public HappyParamet hapines;
    public Transform lookingTo;
    // Start is called before the first frame update
    void Start()
    {
        while (lookingTo == null)
        {
            hapines = GameObject.FindGameObjectWithTag("Happy").GetComponent<HappyParamet>();

            lookingTo = hapines.axisRoot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookingTo);

    }
}
