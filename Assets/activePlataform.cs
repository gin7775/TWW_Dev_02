using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activePlataform : MonoBehaviour
{
    ArmoreBoss armoreBoss;
    Animator animator;
    void Start()
    {
        armoreBoss = GameObject.Find("ArmoreBos").GetComponent<ArmoreBoss>();
        animator = GameObject.Find("Valla 1").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(armoreBoss.currentHealth <= 0)
        {
            animator.SetTrigger("ActivePlataform");
        }
    }
}
