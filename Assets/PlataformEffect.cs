using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformEffect : MonoBehaviour
{
   // Animator animator;
    public int stateFunction;
    public bool switchState,colliderBlocker;
    public GameObject [] asignedPlataforms1;
    public GameObject[] asignedPlataforms2;
    public AudioSource activeSound;

    void Start()
    {
        activeSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPlatforms()
    {
        if (switchState == false)
        {
            for (int i = 0; i < asignedPlataforms1.Length; i++)
            {
                asignedPlataforms1[i].GetComponent<Animator>().SetTrigger("Active");
            }
            for (int i = 0; i < asignedPlataforms2.Length; i++)
            {
                asignedPlataforms1[i].GetComponent<Animator>().SetTrigger("InActive");
            }
            switchState = true;

        }
        else
        {
            for (int i = 0; i < asignedPlataforms1.Length; i++)
            {
                asignedPlataforms1[i].GetComponent<Animator>().SetTrigger("InActive");
            }
            for (int i = 0; i < asignedPlataforms2.Length; i++)
            {
                asignedPlataforms1[i].GetComponent<Animator>().SetTrigger("Active");
            }
            switchState = false;
        }
        

    }
    public void Active()
    {
        for (int i = 0; i < asignedPlataforms1.Length; i++)
        {
            asignedPlataforms1[i].GetComponent<Animator>().SetTrigger("Active");
        }
        
    }
    public void TriggerPlataforms()
    {

        if (stateFunction == 1)
        {
            SwitchPlatforms();
            activeSound.pitch = 1f;
        }
        else if (stateFunction == 2)
        {
            Active();
            activeSound.pitch = 0.6f;

        }
        activeSound.Play();
    }

    /*
    public void plataformEffect()
    {
        GameObject.FindGameObjectWithTag("Plataform").GetComponent<Animator>().SetBool("Effect", true);


    }
    public void plataformEffect1()
    {
        GameObject.Find("Plataform1").GetComponent<Animator>().SetBool("Effect1", true);


    }
    public void plataformEffect2()
    {
        GameObject.Find("Plataform2").GetComponent<Animator>().SetBool("Effect2", true);


    }
    public void plataformEffect3()
    {
        GameObject.Find("Plataform3").GetComponent<Animator>().SetBool("Effect3", true);


    }*/
}
