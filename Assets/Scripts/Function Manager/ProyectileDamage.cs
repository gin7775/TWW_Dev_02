using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileDamage : MonoBehaviour
{
    public int damage = 25;
    public bool bobo;
    public float boboTimer,startTimer;

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

        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);
            Debug.Log("Has tocado");
        }
        else if (other.gameObject.tag == "Turret") 
        {
            boboTimer = startTimer;
            bobo = true;
        }

        else if (bobo == false)
        {
            Debug.Log("Bobo que mira Bobo");
            Destroy(this.gameObject);

        }
        
    }
}
