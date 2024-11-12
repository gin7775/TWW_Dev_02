using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveProyectile : MonoBehaviour
{
    SpellScript SpellScript;

    private void Start()
    {
        SpellScript = GameObject.Find("_PlayerController").GetComponent<SpellScript>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            SpellScript.ProjectilesUnlocked = true;



        }





    }
}
