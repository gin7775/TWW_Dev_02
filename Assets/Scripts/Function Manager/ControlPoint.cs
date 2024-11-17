using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ControlPoint : MonoBehaviour
{
    public bool canTriger;
    public SceneInfo SceneInfo;
    
    public DataPersistenceManager dataPersistenceManager;

    // Se usa para decirle al SceneInfo cuál es este punto 
    public int pointIndex;
    private bool isInTrigger = false;
    public GameObject panelSave;

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
            panelSave.SetActive(true);
            isInTrigger = true; // Marca que el jugador está dentro del trigger
        }
    }

    public void OnInteract(InputValue value)
    {
        // Detecta si la tecla F fue presionada
        if (isInTrigger && value.isPressed) // Solo si está dentro del trigger y la tecla fue presionada
        {
            SceneInfo.controlPoint = pointIndex; // Actualiza el controlPoint
            dataPersistenceManager.SaveGame(); // Guarda el juego
            Debug.Log("Juego guardado en el punto de control: " + pointIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            panelSave.SetActive(false);
            isInTrigger = false; // Marca que el jugador salió del trigger
        }
    }
}