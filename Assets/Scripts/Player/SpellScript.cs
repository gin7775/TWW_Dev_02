using Michsky.UI.Dark;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellScript : MonoBehaviour
{
    public GameObject firePoint;
    public float cooldownTime;
    public bool isCooldown = false;
    public bool SpellOn = false;
    private float cooldownTimer = 0f;
    public GameObject[] proyectiles; // Array de proyectiles
    public GameObject[] effectSpawnProjectile;
    public GameObject holder;
    Player player;
    EnemyLock enemyLock;
    Animator animator;
    [SerializeField] private AudioClip sfxShoot;
   
    private int currentProjectileIndex = 0;
    private int currentMuzzleIndex = 0;// Índice del proyectil actual
    DissolveProyectile dissolveProyectile;
    private bool isDissolving = false;
    public GameObject[] projectileImages;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        enemyLock = GetComponent<EnemyLock>();

       // dissolveProyectile = projectileImages[currentProjectileIndex].gameObject.GetComponent<DissolveProyectile>();

        
    }

    // Update is called once per frame
    void Update()
    {
        ManageCooldown();

        //dissolveProyectile = projectileImages[currentProjectileIndex].gameObject.GetComponent<DissolveProyectile>();

        SpellActivate();

       
    }
    private void ManageCooldown()
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

        if (player.spellOn && !player.isAttacking && !player.isDodging)
        {

            RotatePlayer();

        }
    }

    public void OnChangeProyectile(InputValue input)
    {
        if (isDissolving)
            return;

        isDissolving = true;
       
        if (dissolveProyectile != null)
        {
            dissolveProyectile.StartDissolve();
        }
       
        dissolveProyectile.OnDissolveComplete += HandleDissolveComplete;
    }

    private void HandleDissolveComplete()
    {
        
        dissolveProyectile.OnDissolveComplete -= HandleDissolveComplete;

        
        currentProjectileIndex = (currentProjectileIndex + 1) % proyectiles.Length;
        currentMuzzleIndex = (currentMuzzleIndex + 1) % effectSpawnProjectile.Length;

        
        //UpdateProjectileUI();

        dissolveProyectile.ResetDissolve();

        isDissolving = false;
    }


    //private void UpdateProjectileUI()
    //{
    //    for (int i = 0; i < projectileImages.Length; i++)
    //    {
    //        if (projectileImages[i] != null)
    //        {
    //            projectileImages[i].gameObject.SetActive(i == currentProjectileIndex);
    //        }
    //    }
    //}
    public void SpellActivate()
    {

        if (player.spellOn)
        {
            player.playerSpeed = 0f;
        }

    }
       

    IEnumerator waitInstantiate()
    {
        if(!isCooldown)
        {

            StartCoroutine(rotateSpellAtack());
        }
        
        yield return new WaitForSeconds(0.4f);
        GameObject vfx;
        GameObject effectSpawn;

        if (firePoint != null && !isCooldown )
        {
            isCooldown = true;
            cooldownTimer = cooldownTime;
           
            // Instancia el proyectil actual
            vfx = Instantiate(proyectiles[currentProjectileIndex], firePoint.transform.position, this.transform.rotation);
            effectSpawn = Instantiate(effectSpawnProjectile[currentMuzzleIndex], firePoint.transform.position, this.transform.rotation);
            MiFmod.Instance.Play("SFX_2d/TirarProyectil");

            Destroy(vfx, 2);
            Destroy(effectSpawn, 2);
        }
    }

    void RotatePlayer()
    {
        if (!enemyLock.isLockOnMode)
        {

            Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            Vector3 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

            Vector3 direction = mouseOnScreen - positionOnScreen;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - player.anglee;

            transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));

        }
       

    }

    IEnumerator rotateSpellAtack()
    {
       
        player.spellOn = true;
        yield return new WaitForSeconds(0.4f);
        player.spellOn = false;

    }
}



