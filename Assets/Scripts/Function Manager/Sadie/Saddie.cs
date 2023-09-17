using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saddie : MonoBehaviour
{

    public Animator sadieIA, sadieAnim;
    public Player player;
    public Transform []proyectyleSpawns;
    public GameObject sadieHolder;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sadieIA = this.gameObject.GetComponent<Animator>();
        

    }

    

    public void Death()
    {
        sadieAnim.SetTrigger("Death");
        sadieIA.SetTrigger("Death");
    }
    public void animAtack1()
    {
        sadieAnim.SetTrigger("Ataque1");
    }
    public void animAtack2()
    {
        sadieAnim.SetTrigger("Ataque2");

    }
    public void animIdle()
    {
        sadieAnim.SetTrigger("Idle");

    }
    public void Atack1()
    {
        sadieIA.SetTrigger("Atack1");

    }
    public void Atack2()
    {
        sadieIA.SetTrigger("Atack2");

    }
    public void Choice()
    {
        sadieIA.SetTrigger("Choice");

    }
    public void SpawnProyectile()
    {

        for(int i = 0; i < proyectyleSpawns.Length; i++)
        {
            GameObject toInstantiate = Instantiate(sadieHolder, proyectyleSpawns[i].position, Quaternion.identity);
        }

    }

}
