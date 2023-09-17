using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;

    public int maxHealth;

    public int currentHealth;

    Animator animatorBoss;

    private Animator anim;

    private CinemachineImpulseSource cinemachineImpulseSource;

    public GameObject vfxHitEffect;

    public GameObject vfxHitEffectFinish;

    public Transform vfxSpawn;

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();

        cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();

        currentHealth = maxHealth;

        animatorBoss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
       
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int SetMaxHealthFromHealthLevel()
    {

        maxHealth = healthLevel * 10;

        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(FrameFreeze(0.05f));

        Instantiate(vfxHitEffect, vfxSpawn.position, Quaternion.identity);
        anim.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            anim.SetTrigger("Death");

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
