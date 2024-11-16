using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Animator cinemachineAnim;
    [SerializeField] private string enterAnimName = "CameraTrigger1"; // Nombre de animación al entrar
    [SerializeField] private string exitAnimName = "FollowCamera"; // Nombre de animación al salir
    private EnemyLock enemyLock;
    private bool isInTrigger = false; // Bandera para determinar si el jugador está dentro del trigger

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            enemyLock = playerObject.GetComponent<EnemyLock>();
        }

        if (enemyLock == null)
        {
            Debug.LogError("EnemyLock component not found on the Player object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isInTrigger) // Solo ejecuta si no estamos ya en el trigger
            {
                Debug.Log("Entars");
                isInTrigger = true; // Marca que el jugador está dentro del trigger
                enemyLock.cameraSwitch = true;
                cinemachineAnim.Play(enterAnimName);
                Debug.Log(enterAnimName);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isInTrigger) // Solo ejecuta si estamos dentro del trigger
            {
                isInTrigger = false; // Marca que el jugador ha salido del trigger
                enemyLock.cameraSwitch = false;
                cinemachineAnim.Play(exitAnimName);
            }
        }
    }
}
