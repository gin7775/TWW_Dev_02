using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPoint : MonoBehaviour
{
    public SceneInfo SceneInfo;
    //public DataPersistenceManager dataPersistenceManager;

    // Punto de control asociado a este script
    public int pointIndex;
    private bool isInTrigger = false;
    public GameObject panelSave;

    // Referencia al efecto de fuego para este checkpoint
    public GameObject fireEffect;

    // Lista estática para controlar todos los checkpoints activos
    private static GameObject currentFireEffect; // Efecto activo actualmente
    private static int currentPointIndex = -1;  // Índice del checkpoint activo

    private void Start()
    {
       // dataPersistenceManager = GameManager.gameManager.dataPersistenceManager;
        SceneInfo = FindObjectOfType<SceneInfo>();

        // Restaura el efecto de fuego si este checkpoint es el último guardado
        if (SceneInfo.controlPoint == pointIndex)
        {
            ActivateFireEffect();
        }
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
        if (isInTrigger && value.isPressed) // Solo si está dentro del trigger y la tecla fue presionada
        {
            SceneInfo.controlPoint = pointIndex;
            GameManager.gameManager.spawnIndex = pointIndex;// Actualiza el controlPoint en SceneInfo
            GameManager.gameManager.dataPersistenceManager.SaveGame();
            //dataPersistenceManager.SaveGame(); // Guarda el juego
            Debug.Log("Juego guardado en el punto de control: " + pointIndex);

            // Activa el fuego en este checkpoint y desactiva en el anterior
            ActivateFireEffect();
        }
    }

    private void ActivateFireEffect()
    {
        // Desactiva el efecto del checkpoint anterior si existe
        if (currentFireEffect != null && currentPointIndex != pointIndex)
        {
            currentFireEffect.SetActive(false);
            Debug.Log($"Desactivado fuego del checkpoint anterior: {currentPointIndex}");
        }

        // Activa el efecto de este checkpoint
        fireEffect.SetActive(true);
        currentFireEffect = fireEffect;
        currentPointIndex = pointIndex;

        Debug.Log($"Activado fuego para el checkpoint: {pointIndex}");
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
