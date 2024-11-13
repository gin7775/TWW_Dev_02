using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileDamage : MonoBehaviour
{
    public int damage = 25;
    public bool bobo;
    public float boboTimer,startTimer;
    public GameObject hitParticlePrefab;
    private void Start()
    {
        boboTimer = startTimer;
        bobo = true;
    }
    private void Update()
    {
        boboTimer -= Time.deltaTime;
        if (boboTimer<= 0)
        {
            bobo = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();

        // Si colisiona con el jugador
        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);
            Debug.Log("Has tocado");

            // Crear la partícula de impacto orientada en la dirección del proyectil
            if (hitParticlePrefab != null)
            {
                // Instancia la partícula en la posición de colisión
                GameObject hitParticle = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);

                // Orienta la partícula en la dirección del proyectil
                hitParticle.transform.rotation = Quaternion.LookRotation(transform.forward);

                Destroy(this.gameObject);
            }
        }
        // Si colisiona con la torreta
        else if (other.gameObject.tag == "Turret")
        {
            boboTimer = startTimer;
            bobo = true;
        }
        // Destruir el proyectil si bobo es falso
        else if (bobo == false)
        {
            Destroy(this.gameObject);
        }
    }


}
