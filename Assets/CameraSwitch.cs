using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Animator cinemachineAnim;
    private EnemyLock enemyLock;

    

    private void Start()
    {
        // Suponiendo que EnemyLock está en el objeto con tag "Player"
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
            enemyLock.cameraSwitch = true; // Activar cameraSwitch en EnemyLock
            cinemachineAnim.Play("CameraTrigger1");
            Debug.Log("Camera1");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyLock.cameraSwitch = false; // Desactivar cameraSwitch en EnemyLock
            cinemachineAnim.Play("FollowCamera");
        }
    }


}
