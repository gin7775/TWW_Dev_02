using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
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
