using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmoreBoss : MonoBehaviour 
{ 
 public int maxHealth, currentHealth, poiseHealth;
    public float speed;
 public Animator iaArmore, animArmore;
 public int guardState;
 public GameObject player, Holder_1, Holder_2;


 public GameObject vfxHitEffect;

 public GameObject vfxHitEffectFinish;

 public Slider slider;

 public Transform vfxSpawn;

 private CinemachineImpulseSource cinemachineImpulseSource;

// Start is called before the first frame update
 void Start()
 {
    guardState = 1;
    player = GameObject.FindGameObjectWithTag("Player");
    cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
 }

// Update is called once per frame
 void Update()
 {
    slider.value = currentHealth;
 }
 public void TakeDamage(int damage)
 {
    if (guardState == 1)
    {
        Debug.Log("recibiria daño");
        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(FrameFreeze(0.05f));

        Instantiate(vfxHitEffect, vfxSpawn.transform.position, Quaternion.identity);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animArmore.SetTrigger("Death");
            Instantiate(vfxHitEffectFinish, vfxSpawn.transform.position, Quaternion.identity);
            iaArmore.SetTrigger("Death");

        }
    }
    else if (guardState == 2)
    {
        //is bloquing

        iaArmore.SetTrigger("HorizontalAtack");
        //animArmore.SetTrigger("Block");
        guardState = 1;
    }
    else if (guardState == 3)
    {
        //is healing
        poiseHealth -= damage;
        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(FrameFreeze(0.05f));

        Instantiate(vfxHitEffect, vfxSpawn.transform.position, Quaternion.identity);
        if (poiseHealth <= 0)
        {
            Instantiate(vfxHitEffectFinish, vfxSpawn.transform.position, Quaternion.identity);
            poiseHealth = 0;
            animArmore.SetTrigger("HealBreak");
            guardState = 1;

        }
        //animArmore.SetTrigger("Healing");

    }

}

 private IEnumerator FrameFreeze(float duration)
 {
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(duration);

    Time.timeScale = 1f;
 }
 //Metodos de cambio de estado necesarios
 public void ArmoreWalk(int value)
 {
    animArmore.SetFloat("Move", value);
 }
 public void ArmoreVertical()
 {
    animArmore.SetTrigger("AtaqueVertical");
 }
 public void ArmoreHorizontal()
 {
    animArmore.SetTrigger("AtaqueHorizontal");

 }
 public void ArmoreBlock()
 {
    animArmore.SetTrigger("Block");

 }
 public void ArmoreHitme()
 {
    iaArmore.SetTrigger("HitMe");
 }
 public void ArmoreHealBreack()
 {
    animArmore.SetTrigger("HealBreak");
 }
 public void ArmoreIdle()
 {
    animArmore.SetTrigger("Idle");
 }
 public void AnimArmoreHeal()
 {
    animArmore.SetTrigger("Healing");
 }
 public void AnimArmoreRecovery()
 {
    animArmore.SetTrigger("HealRecovery");
 }
 public void ArmoreAction()
 {
    iaArmore.SetTrigger("Action");
 }
 public void ArmoreActivate()
 {
    animArmore.SetTrigger("Activate");
 }
}
