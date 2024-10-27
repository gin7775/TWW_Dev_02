using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarEscena1 : MonoBehaviour
{
    public int sceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.gameManager.death = false;
            Debug.Log("Estoy entrando al colider de sceneInfo");
            GameManager.gameManager.spawnIndex = 0;
            GameManager.gameManager.NextScene(sceneIndex);
        }
    }
}
