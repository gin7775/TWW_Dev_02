using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : MonoBehaviour
{
    public string playerLayerName = "Player";

    void Start()
    {
        // Obtener el número de capa correspondiente al nombre de la capa del jugador
        int playerLayer = LayerMask.NameToLayer(playerLayerName);
        int projectileLayer = gameObject.layer;

        // Ignorar colisiones entre la capa del jugador y la capa del proyectil
        Physics.IgnoreLayerCollision(projectileLayer, playerLayer);

    }


    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisión es con la capa "Player"
        if (collision.gameObject.layer == LayerMask.NameToLayer(playerLayerName))
        {
            return; // Ignorar la colisión si es con la capa del jugador
        }

        // Aquí puedes manejar otras colisiones si es necesario
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer(playerLayerName))
        {
            return; // Ignorar la colisión si es con la capa del jugador
        }
        if (other.tag == "Armore")
        {
            ArmoreBoss armore = other.GetComponent<ArmoreBoss>();

            Debug.Log("Detecta al menos");

            if (armore != null)
            {
                armore.TakeDamage(5);
            }
        }
        else if (other.tag == "Happy")
        {
            HappyParamet happynes = other.GetComponent<HappyParamet>();

            Debug.Log("Detecta al menos");

            if (happynes != null)
            {
                happynes.TakeDamage(5);
            }
        }
        else if (other.tag == "Candela")
        {
            Candela candela = other.GetComponent<Candela>();
            if (candela != null)
            {
                candela.TakeDamage(5);
            }


        }


        else if (other.tag == "Boss")
        {
            BossFinal armore = other.GetComponent<BossFinal>();

            Debug.Log("Detecta al menos");

            if (armore != null)
            {
                armore.TakeDamage(5);
            }
        }

        else if (other.tag == "Puppet")
        {
            Puppet puppet = other.GetComponent<Puppet>();
            
            Debug.Log("Detecta al menos");

            if (puppet != null)
            {
                puppet.TakeDamage(25);
            }
        }
        else if (other.tag == "Tarik")
        {
            TarikEnemy tarikEnemy = other.GetComponent<TarikEnemy>();

            Debug.Log("Detecta al menos");

            if (tarikEnemy != null)
            {
                tarikEnemy.TakeDamage(25);
            }
        }
        else if (other.tag == "Empresario")
        {
            EmpresarioScript puppet = other.GetComponent<EmpresarioScript>();

            Debug.Log("Detecta al menos");

            if (puppet != null)
            {
                puppet.TakeDamage(25);
            }
        }

        else if (other.tag == "platformTrigger")
        {
            PlataformEffect plataform = other.GetComponent<PlataformEffect>();
            //insertar sonido de piano
            Debug.Log("Detecta");

            plataform.TriggerPlataforms();
        }
       
    }
}
