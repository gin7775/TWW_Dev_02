using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedorPuppet : MonoBehaviour
{

    public Transform[] destination;
    public int nextPosition;
    public int maxPosition;

    public float waitingTimeAttacking;

    public float waitingTimeHitme;

    public GameObject projectiles;

    public bool projectilesLock;

    public Animator animPuppet;

}
