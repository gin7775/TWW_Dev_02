using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ControlPoint : MonoBehaviour
{
    public bool canTriger;
    public SceneInfo SceneInfo;
    private string saveKey = "f";
    public DataPersistenceManager dataPersistenceManager;
    //Se usa para decirle al scene info cual es este punto 
    public int pointIndex;

    private void Start()
    {
        canTriger = true;
        dataPersistenceManager = DataPersistenceManager.instance;
        SceneInfo = FindObjectOfType<SceneInfo>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
           // Debug.Log("Estoy en collider");
            if (UnityEngine.Input.GetKeyDown(saveKey))
            {
                SceneInfo.controlPoint = pointIndex;
                dataPersistenceManager.SaveGame();
                Debug.Log("La tecla F ha sido presionada y por tanto el juego salvado");

            }
        }
        
    }
}
