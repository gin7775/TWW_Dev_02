using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;

    GameObject damageCollider2;

    public int currentWeaponDamage = 25;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();

        
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;

    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }
    


    private void OnTriggerEnter(Collider other)
    {
      

         if (other.tag == "Armore")
        {
            ArmoreBoss armore = other.GetComponent<ArmoreBoss>();

            Debug.Log("Detecta al menos");

            if (armore != null)
            {
                armore.TakeDamage(10);
            }
        }
        else if (other.tag == "Happy")
        {
            HappyParamet happynes = other.GetComponent<HappyParamet>();

            Debug.Log("Detecta al menos");

            if (happynes != null)
            {
                happynes.TakeDamage(10);
            }
        }
        else if (other.tag == "Candela")
        {
            Candela candela = other.GetComponent<Candela>();
            if (candela != null)
            {
                candela.TakeDamage(10);
            }
          
            
        }


        else if (other.tag == "platformTrigger")
        {
            PlataformEffect plataform = other.GetComponent<PlataformEffect>();

            Debug.Log("Detecta");

            plataform.TriggerPlataforms();
        }
        else if (other.tag == "Boss")
        {
            BossFinal armore = other.GetComponent<BossFinal>();

            Debug.Log("Detecta al menos");

            if (armore != null)
            {
                armore.TakeDamage(10);
            }
        }

        else if (other.tag == "Puppet")
        {
            Puppet puppet = other.GetComponent<Puppet>();

            
            if (puppet != null)
            {
                puppet.TakeDamage(50);
            }

        }
        else if (other.tag == "Empresario")
        {
            EmpresarioScript empresario = other.GetComponent<EmpresarioScript>();


            if (empresario != null)
            {
                empresario.TakeDamage(50);
            }
        }
        else if (other.tag == "Saddie")
        {
            SaddieHeath saddie = other.GetComponent<SaddieHeath>();


            if (saddie != null)
            {
                saddie.TakeDamage(50);
            }
        }
        else if (other.tag == "SkullCrow")
        {
            SkullDamage crowSkullhit = other.GetComponent<SkullDamage>();


            if (crowSkullhit != null)
            {
                crowSkullhit.killCrow();
            }
        }

        else if (other.tag == "Tarik")
        {
            TarikEnemy tarik = other.GetComponent<TarikEnemy>();

            if (tarik != null)
            {
                tarik.TakeDamage(50);
            }

        }
    }

   
}
