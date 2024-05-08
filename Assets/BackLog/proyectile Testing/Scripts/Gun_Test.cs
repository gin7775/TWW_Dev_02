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
            GameObject x = Instantiate(projectile, this.transform.position, Quaternion.identity);
            rb = x.GetComponent<Rigidbody>();
            localReference = this.transform.forward;
            rb.AddForce(localReference * speed, ForceMode.VelocityChange);
            shoots--;
            Debug.Log("Dispara");
        }
        
        yield return new WaitForSeconds(fireRate);
        if(shoots > 0)
        {
            StartCoroutine(Shooting());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
}
