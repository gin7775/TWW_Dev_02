using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappyParamet : MonoBehaviour
{
    public Transform[] parametSpawnPos;
    public GameObject[] armoreServant;
    public float maxHealth, currentHealth;
    public Transform axisRoot;
    private Animator iaController;
    public GameObject[] proyectileHolders;


    private CinemachineImpulseSource cinemachineImpulseSource;

    public GameObject vfxHitEffect;

    public GameObject vfxHitEffectFinish;
    public Slider slider;
    public Transform vfxSpawn;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
        currentHealth = maxHealth;
        iaController = this.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        armoreServant = GameObject.FindGameObjectsWithTag("Armore");
        slider.value = currentHealth;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("recibiria daño");

        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(FrameFreeze(0.05f));

        Instantiate(vfxHitEffect, vfxSpawn.position, Quaternion.identity);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            iaController.SetTrigger("Death");
            Instantiate(vfxHitEffectFinish, vfxSpawn.transform.position, Quaternion.identity);
        }

    }


    private IEnumerator FrameFreeze(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
    }
}
