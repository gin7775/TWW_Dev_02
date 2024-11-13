using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Test : MonoBehaviour
{

    private Vector3 localReference;
    public int shoots;
    public float fireRate;
    public GameObject projectile,player;
    public float speed = 3;
    public Rigidbody rb;
    public bool toPlayer;
    public bool cantShoot;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       StartCoroutine(Shooting());     
    }

    IEnumerator Shooting()
    {
        if (cantShoot == false)
        {
            if (toPlayer == true)
            {
                transform.LookAt(player.transform.position);
            }

            // Instanciar el proyectil en la posición y rotación inicial
            GameObject x = Instantiate(projectile, transform.position, Quaternion.identity);
            rb = x.GetComponent<Rigidbody>();

            // Obtener la dirección de disparo
            localReference = transform.forward;

            // Apuntar el proyectil en la dirección de disparo
            x.transform.rotation = Quaternion.LookRotation(localReference);

            // Aplicar fuerza en la dirección de disparo
            rb.AddForce(localReference * speed, ForceMode.VelocityChange);

            // Disminuir el número de disparos restantes
            shoots--;
            
        }

        yield return new WaitForSeconds(fireRate);

        if (shoots > 0)
        {
            StartCoroutine(Shooting());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
}
