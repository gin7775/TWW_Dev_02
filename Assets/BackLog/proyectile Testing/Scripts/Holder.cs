using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Holder : MonoBehaviour
{
    public GameObject circleProyectile;
    public int circleNumberMax, circleCount;
    public List<GameObject> circles; // Cambiamos de arreglo a lista

    public GameObject Player;
    public Gun_Test[] Guns;
    public bool needPlayer;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Guns = GetComponentsInChildren<Gun_Test>();

        if (needPlayer && Player != null)
        {
            transform.LookAt(Player.transform.position);
        }

        circles = new List<GameObject>(); // Inicializamos la lista
    }

    public void SetGun(int shoots, float fireRate, float speed, bool track)
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
            circles.Add(x); // Agregamos el nuevo círculo a la lista
            x.GetComponent<Gunner_Test>().proyectilePos = Guns[i].transform.position;
            x.GetComponent<Gunner_Test>().secondFace = false;
            x.GetComponent<Gunner_Test>().CircleGun();
        }
        circleCount++;
        if (circleCount >= circleNumberMax)
        {
            Debug.Log("shouldDestroy");
            for (int i = 0; i < circles.Count; i++) // Iteramos sobre la lista
            {
                Destroy(circles[i]); // Destruimos cada círculo en la lista
            }
            circles.Clear(); // Limpiamos la lista después de destruir los círculos
            Destroy(this.gameObject);
        }
    }
}
