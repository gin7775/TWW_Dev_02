using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMusicBoss : MonoBehaviour
{

    public bool activeSoundBoss = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))            
        {
           
            activeSoundBoss = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))

        {

            MiFmod.Instance.StopFondo();

        }
    }
}
