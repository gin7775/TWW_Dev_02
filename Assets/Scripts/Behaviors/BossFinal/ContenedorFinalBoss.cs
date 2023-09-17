using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedorFinalBoss : MonoBehaviour
{
    public float currentTimeAttacks;
    public float startingTimeAttacks;

    public float currentTimeGolpeEspejismo;
    public float startingTimeGolpeEspejismo;

    public float currentTimeGolpeTornado;
    public float startingTimeGolpeTornado;

    public float currentTimeAtaque1;
    public float startingTimeAtaque1;

    public float timeDamage;

    public float currentTimeDescanso;

    public float startingTimeDescanso;

    public Transform[] wayPoints;

    public int nextWayPoint;

    public float distanceToPlayer;

    public GameObject player;

    public GameObject fire;

    public bool ataque1;

    public Animator anim;

    public Vector3 SlashGroundPos;

    public Transform fireGroundSlash;

    public GameObject[] SlashPrefab;
   

    public GameObject gameObjectSon;

    public Transform[] TornadoPoints;
    public GameObject[] tornado;

    public Transform[] puntosCortesSalvajes;

}
