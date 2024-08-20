using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ContainerTarik : MonoBehaviour
{
    
    public List<Transform> waypoints;  // Cambiado a una lista de Transform
    public int currentWaypointIndex;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    public GameObject projectilePrefab;
    public Transform SpawnPosition;
    public float initialVelocity = 10f; // Ajusta esta velocidad según sea necesario
    public float angle = 45f;
    public float jumpTriggerDistance = 2f;
    public float jumpDelay = 1.0f;
    public Transform PlayerTransform;
    public bool isGrounded;
    public List<Transform> jumpPoints;
    public float jumpHeight;
    public float jumpDuration;
  
    public Animator animatorTarik;

    public IEnumerator ParabolicJump(Transform objectToMove, Vector3 destination)
    {
        Vector3 startPosition = objectToMove.position;
        float elapsed = 0;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / jumpDuration;
            float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;

            objectToMove.position = Vector3.Lerp(startPosition, destination, t) + Vector3.up * height;

            yield return null;
        }

        objectToMove.position = destination; // Asegúrate de que el objeto llegue al destino
    }

   
}
