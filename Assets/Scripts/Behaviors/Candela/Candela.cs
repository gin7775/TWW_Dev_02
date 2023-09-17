using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Candela : MonoBehaviour
{
    public Transform[] spinPoints;
    public GameObject player;
    public GameObject[] ballerinas;
    public GameObject proyectile;
    public int maxHealth;
    public int currentHealth;
    public Animator animations;
    private CinemachineImpulseSource cinemachineImpulseSource;

    public GameObject vfxHitEffect;

    public GameObject vfxHitEffectFinish;

    public Transform vfxSpawn;

    public Slider slider; 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ballerinas = GameObject.FindGameObjectsWithTag("Ballerina");
        cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
    }
    public void StartBallerinas()
    {
        foreach (GameObject ballerina in ballerinas)
        {
            ballerina.GetComponent<Animator>().SetTrigger("Start");
        }
    }
    public void BallerinaDance()
    {
        foreach (GameObject ballerina in ballerinas)
        {
            ballerina.GetComponent<Animator>().SetBool("leave", false);
            ballerina.GetComponent<Ballerinas>().BallerinaStartDance();
        }
    }
    public void BallerinaLeave()
    {
        foreach (GameObject ballerina in ballerinas)
        {
            ballerina.GetComponent<Animator>().SetBool("dance", false);
            ballerina.GetComponent<Animator>().SetBool("leave", true);
        }
    }
    public void Update()
    {
        slider.value = currentHealth;
    }
    public void BallerinaReturn()
    {
        foreach (GameObject ballerina in ballerinas)
        {
            ballerina.GetComponent<Animator>().SetBool("leave", false);
            ballerina.GetComponent<Animator>().SetBool("return", true);
        }
    }
    public void TakeDamage(int damage)
    {
        if (this.GetComponent<Animator>().GetBool("Attack1"))
        {
            this.GetComponent<Animator>().SetTrigger("Hitted");
            currentHealth -= damage;

            cinemachineImpulseSource.GenerateImpulse();
            StartCoroutine(FrameFreeze(0.05f));
            Instantiate(vfxHitEffect, vfxSpawn.position, Quaternion.identity);

            Debug.Log("Has hecho daño");
            if (currentHealth <= 0)
            {
                BallerinaLeave();
                animations.SetTrigger("Death");
                player.GetComponent<Player>().collectedKeys++;
                Instantiate(vfxHitEffectFinish, vfxSpawn.position, Quaternion.identity);
            }
        }
    }
    private IEnumerator FrameFreeze(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
    }
    public void DeathBallerinas()
    {
        foreach (GameObject ballerina in ballerinas)
        {
            ballerina.GetComponent<Animator>().SetTrigger("Death");
        }
    }
}
