using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TarikEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject playerReference;
    public float waitingTime;

    public float currentHealth;

    public GameObject vfxHitEffect;

    public GameObject vfxHitEffectFinish;

    public GameObject vfxSpawn;

    public GameObject vfxSpawnBlood;

    public GameObject vfxBlood;

    private CinemachineImpulseSource cinemachineImpulseSource;

    ContainerTarik contenedorTarik;

    public Slider sliderVida;

    [SerializeField] private AudioClip sfxGolpe;

    public float knockbackDistance = 1.5f;
    public float knockbackDuration = 0.3f; // Duración del retroceso
    public float angulo;


    // Start is called before the first frame update
    void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");

        cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
        DOTween.Init();
        contenedorTarik = this.GetComponent<ContainerTarik>();
        player = playerReference.transform;
    }

    // Update is called once per frame
    void Update()
    {
        sliderVida.value = currentHealth;


        angulo = this.transform.localEulerAngles.y;

    }



    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(FrameFreeze(0.07f));
        ControladorSonidos.Instance.EjecutarSonido(sfxGolpe);

        Vector3 attackDirection = (player.position - transform.position).normalized;
        Vector3 oppositeDirection = -attackDirection;

        // Instanciar el VFX de sangre en la dirección opuesta
        Instantiate(vfxHitEffect, vfxSpawn.transform.position, Quaternion.identity);
        Instantiate(vfxBlood, vfxSpawnBlood.transform.position, Quaternion.LookRotation(oppositeDirection));
        ApplyKnockback();

        contenedorTarik.animatorTarik.SetTrigger("Hit");

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

    private void ApplyKnockback()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 knockbackPosition = transform.position - directionToPlayer * knockbackDistance;

        // Animar el movimiento de retroceso
        transform.DOMove(knockbackPosition, knockbackDuration).SetEase(Ease.OutExpo);
    }
}
