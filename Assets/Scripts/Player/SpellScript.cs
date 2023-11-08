using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellScript : MonoBehaviour
{
    public GameObject firePoint;
    public float cooldownTime;
    public bool isCooldown = false;
    private float cooldownTimer = 0f;
    public GameObject[] proyectiles; // Array de proyectiles
    public GameObject[] effectSpawnProjectile;
    public GameObject holder;
    Player player;
    Animator animator;
    [SerializeField] private AudioClip sfxShoot;

    private int currentProjectileIndex = 0;
    private int currentMuzzleIndex = 0;// Índice del proyectil actual

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
            }
        }
    }

    public void OnRotate(InputValue input)
    {
        if (!isCooldown && !player.spellOn)
        {
            animator.SetTrigger("Spell");
            StartCoroutine(waitInstantiate());
        }
    }

    public void OnChangeProyectile(InputValue input)
    {
        
        currentProjectileIndex = (currentProjectileIndex + 1) % proyectiles.Length;
       // Debug.Log("Cambiado a proyectil " + currentProjectileIndex);
        currentMuzzleIndex = (currentMuzzleIndex + 1) % effectSpawnProjectile.Length;
    }

    IEnumerator waitInstantiate()
    {
        yield return new WaitForSeconds(0.4f);
        GameObject vfx;
        GameObject effectSpawn;

        if (firePoint != null && !isCooldown && player.spellOn)
        {
            isCooldown = true;
            cooldownTimer = cooldownTime;
            // Instancia el proyectil actual
            vfx = Instantiate(proyectiles[currentProjectileIndex], firePoint.transform.position, this.transform.rotation);
            effectSpawn = Instantiate(effectSpawnProjectile[currentMuzzleIndex], firePoint.transform.position, this.transform.rotation);
            ControladorSonidos.Instance.EjecutarSonido(sfxShoot);

            Destroy(vfx, 2);
            Destroy(effectSpawn, 2);
        }
    }
}



