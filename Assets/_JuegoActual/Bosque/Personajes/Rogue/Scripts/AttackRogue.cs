using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRogue : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public GameObject projectilePrefab; // Prefab del proyectil
    public float speed = 5f;
    public Transform vfxSppawn;

    // Método para lanzar el proyectil
    public void LaunchProjectile()
    {
        // Crear una instancia del proyectil
        GameObject projectile = Instantiate(projectilePrefab, vfxSppawn.position, Quaternion.identity);

        
        
    }

    // Corutina para mover el proyectil
    
}
