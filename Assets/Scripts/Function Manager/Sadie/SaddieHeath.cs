using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaddieHeath : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;

    //Animator animatorBoss;

    public Saddie payaso;

    public Player player;

    private CinemachineImpulseSource cinemachineImpulseSource;

    public GameObject vfxHitEffect;

    public GameObject vfxHitEffectFinish;

    public Transform vfxSpawn;

    public Slider slider;

    void Start()
    {

        cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
        currentHealth = maxHealth;

        payaso = this.gameObject.GetComponent<Saddie>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(FrameFreeze(0.05f));

        Instantiate(vfxHitEffect, vfxSpawn.position, Quaternion.identity);

        //anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //animatorBoss.SetTrigger("Death");
            payaso.Death();
            player.collectedKeys++;
            Instantiate(vfxHitEffectFinish, vfxSpawn.position, Quaternion.identity);
        }
    }

    private IEnumerator FrameFreeze(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
    }
}
