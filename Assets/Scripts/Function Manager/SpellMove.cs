using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    [SerializeField] private AudioClip sfxExplosion;
    public GameObject hitEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0f;

        ContactPoint contact = collision.contacts[0];

        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);

        Vector3 pos = contact.point;

       if(hitEffect != null)
        {
            var HitVFX = Instantiate(hitEffect, pos, rot);
            ControladorSonidos.Instance.EjecutarSonido(sfxExplosion);
            Destroy(HitVFX, 2);
        }

        Destroy(this.gameObject);


        
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.tag == "platformTrigger")
        {
            PlataformEffect plataform = other.GetComponent<PlataformEffect>();

            Debug.Log("Detecta");

            plataform.TriggerPlataforms();
        }
    }
}
