using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarik : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform SpawnPosition;
    public float angle = 45f;
    public float initialVelocity = 10f;
    public Transform PlayerTransform;
    Vector3 CalculateProjectileVelocity(Transform source, Transform target, float initialVelocity, float angle)
    {
        Vector3 direction = target.position - source.position;
        float yOffset = direction.y;  // Altura relativa del objetivo
        direction.y = 0;  // Ignorar la altura para calcular la distancia horizontal
        float distance = direction.magnitude;  // Distancia horizontal

        // Convertir ángulo a radianes
        float radiansAngle = Mathf.Deg2Rad * angle;

        // Asegurarse de que el ángulo no es ni 0 ni 90 grados para evitar la división por cero
        if (Mathf.Approximately(radiansAngle, 0) || Mathf.Approximately(radiansAngle, Mathf.PI / 2))
        {
            
            return Vector3.zero;
        }

        // Asegurarse de que no estamos tomando la raíz cuadrada de un número negativo
        float term = Physics.gravity.magnitude * distance / Mathf.Sin(2 * radiansAngle);
        if (term < 0)
        {
            
            return Vector3.zero;
        }

        float velocityX = Mathf.Sqrt(term);
        direction.y = distance * Mathf.Tan(radiansAngle) + yOffset;  // Establecer la altura basada en el ángulo y la distancia
        return velocityX * direction.normalized;
    }

    public void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, SpawnPosition.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = CalculateProjectileVelocity(transform, PlayerTransform, initialVelocity, angle);
    }
}
