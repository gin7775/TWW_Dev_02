using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public Transform[] proyectyleSpawns;
    public GameObject sadieHolder;
    public void SpawnProyectile()
    {

        for (int i = 0; i < proyectyleSpawns.Length; i++)
        {
            GameObject toInstantiate = Instantiate(sadieHolder, proyectyleSpawns[i].position, Quaternion.identity);
        }

    }
}
