using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSlashMuppet : MonoBehaviour
{
    public ParticleSystem slashDerecha;
    public ParticleSystem slashIzquierda;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveSlashDerecha()
    {
        slashDerecha.Play();
    }
    public void ActiveSlashIzuierda()
    {
        slashIzquierda.Play();
    }
}
