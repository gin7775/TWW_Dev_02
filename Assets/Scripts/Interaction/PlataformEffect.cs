using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformEffect : MonoBehaviour
{
    // Animator animator;
    public int stateFunction;
    public bool switchState, colliderBlocker;
    public GameObject[] asignedPlataforms1;
    public GameObject[] asignedPlataforms2;
    // Agrega un array con los nombres de los SFX que quieres reproducir
    public string[] sfxNames;


    void Start()
    {
        
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
           
        }
        else if (stateFunction == 2)
        {
            Active();
            

        }

        if (sfxNames.Length > 0)
        {
            int index = Random.Range(0, sfxNames.Length); // Selecciona un índice aleatorio
            MiFmod.Instance.Play(sfxNames[index]); // Reproduce el SFX seleccionado
        }
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
