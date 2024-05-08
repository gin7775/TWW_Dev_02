using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public GameObject circleProyectile;
    public int circleNumberMax,circleCount;
    public GameObject[] circles;

    public GameObject Player;
    public Gun_Test[] Guns;
    public bool needPlayer;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Guns = GetComponentsInChildren<Gun_Test>();
        
        if(needPlayer == true && Player != null)
        {
            this.transform.LookAt(Player.transform.position);
        }
        
    }
    public void SetGun(int shoots, float fireRate, float speed,bool track)
    {
        for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].shoots = shoots;
            Guns[i].fireRate = fireRate;
            Guns[i].speed = speed;
            Guns[i].toPlayer = track;
        }
    }
    public void StartShoot()
    {
        for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].cantShoot = false;
        }
    }
    public void CircleTurrets()
    {
        for (int i = 0; i < Guns.Length; i++)
        {
            GameObject x = Instantiate(circleProyectile, Guns[i].transform.position, Quaternion.identity);
            circles[i] = x;
            circles[i].GetComponent<Gunner_Test>().secondFace = false;
            circles[i].GetComponent<Gunner_Test>().CircleGun();
        }
        circleCount++;
        if (circleCount >= circleNumberMax)
        {
            Debug.Log("shouldDestroy");
            for (int i = 0; i < Guns.Length; i++)
            {

                Destroy(circles[i]);
            }
        }
    }
}
