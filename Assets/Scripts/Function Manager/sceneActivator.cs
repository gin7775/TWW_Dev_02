using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneActivator : MonoBehaviour
{
    public GameObject Player,blockEntry;
    public int enemyCount;
    public bool armoreRoom;
    //Por si se nos ocurre la locura de poner mas de uno en una habitacion.
    public Animator [] armoreEnemys;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.gameObject;
            ScenneActivate();
        }
    }
    public void ScenneActivate()
    {
        
       
        if (armoreRoom == true)
        {
            for(int i = 0; i < armoreEnemys.Length; i++)
            {
                armoreEnemys[i].SetTrigger("Activate");
            }
        }

    }
}
