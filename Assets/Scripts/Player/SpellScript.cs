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

    public GameObject proyectile;

    public GameObject effectSpawnProjectile;

    Animator animator;
    [SerializeField] private AudioClip sfxShoot;
    Player player;

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
        }
       
        StartCoroutine(waitInstantiate());

       
    }

    

    IEnumerator waitInstantiate()
    {

        yield return new WaitForSeconds(0.4f);

        GameObject vfx;
        GameObject effectSpawn;

        if (firePoint != null && !isCooldown && player.spellOn )
        {
            isCooldown = true;
            
            cooldownTimer = cooldownTime;
            vfx = Instantiate(proyectile, firePoint.transform.position, this.transform.rotation);
            effectSpawn = Instantiate(effectSpawnProjectile, firePoint.transform.position, this.transform.rotation);
            ControladorSonidos.Instance.EjecutarSonido(sfxShoot);

            Destroy(vfx, 2);
            Destroy(effectSpawn, 2);
            
        }
        
    }
}
