using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public GameObject Player;
    public Gun_Test[] Guns;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Guns = GetComponentsInChildren<Gun_Test>();

        this.transform.LookAt(Player.transform.position);
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
}
