using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarikProyectile : MonoBehaviour
{
    
    public GameObject impactParticlePrefab;
    private void OnTriggerEnter(Collider other)
    {
       
        // Comprobar si el proyectil ha chocado con el suelo
        if (other.gameObject.CompareTag("Ground")) // Asegúrate de que el suelo tenga el tag "Ground"
        {
            InstantiateImpactParticle();
            Destroy(this.gameObject);
        }
    }

    void InstantiateImpactParticle()
    {
        if (impactParticlePrefab != null)
        {
            GameObject impactParticle = Instantiate(impactParticlePrefab, transform.position, Quaternion.identity);
            Destroy(impactParticle, 0.5f); // Destruye la partícula después de un tiempo para evitar acumulación
        }
    }
}

