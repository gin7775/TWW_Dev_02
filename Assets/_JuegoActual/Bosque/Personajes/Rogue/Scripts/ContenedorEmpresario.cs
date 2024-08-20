using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContenedorEmpresario : MonoBehaviour
{
   public  Animator animEmpresario;


    public Transform[] destination;
    public int nextPosition;
    public int maxPosition;

    
  

    public GameObject projectiles;

    public bool projectilesLock;

    public Transform spawnProyectile;

    public float coolDown;
   

    public bool coolDownAnim;

    public float coolDown3;

    public float coolDownRun;
}
