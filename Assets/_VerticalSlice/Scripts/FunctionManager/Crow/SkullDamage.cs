using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDamage : MonoBehaviour
{
    public CrowDirector directorIa;
    public GameObject atatchedCrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void killCrow() 
    { 
    
        atatchedCrow.GetComponent<Animator>().SetTrigger("Muerte");

       // colocar al final de la animacion de muerte del cuervo directorIa.maskChange();
    
    }
}
