using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmpresarioScript : MonoBehaviour
{
    public Transform player;
    public GameObject holder, playerReference;
    public float waitingTime;

    public float currentHealth;

    public GameObject vfxHitEffect;

    public GameObject vfxHitEffectFinish;

    public GameObject vfxSpawn;
    public GameObject vfxSpawnBlood;

    public GameObject vfxBlood;
    private CinemachineImpulseSource cinemachineImpulseSource;

    [SerializeField] private AudioClip sfxHit;

    ContenedorEmpresario contenedorEmpresario;
    public float angulo;
    public Slider sliderVida;

    // Start is called before the first frame update
    void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");

        cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
        
        contenedorEmpresario = this.GetComponent<ContenedorEmpresario>();
        player = playerReference.transform;
    }

    // Update is called once per frame
    void Update()
    {
        sliderVida.value = currentHealth;

        angulo = this.transform.localEulerAngles.y;

    }

    public void LaunchProyectile()
    {
        GameObject toInstantiate = GameObject.Instantiate(holder);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(FrameFreeze(0.05f));


        ControladorSonidos.Instance.EjecutarSonido(sfxHit);
        Instantiate(vfxHitEffect, vfxSpawn.transform.position, Quaternion.identity);

        Instantiate(vfxBlood, vfxSpawnBlood.transform.position, Quaternion.Euler(0, -angulo, 0));

        contenedorEmpresario.animEmpresario.SetTrigger("ataque");

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Destroy(this.gameObject, 0.3f);
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
