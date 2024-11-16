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
    public List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
    public float blinkIntensity;
    public float blinkDuration;

    // Start is called before the first frame update
    void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(UpdateSliderOrientation());
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
        StartCoroutine(FrameFreeze(0.1f));
        MiFmod.Instance.Play("SFX_2d/HeridoTarik");

        Vector3 attackDirection = (player.position - transform.position).normalized;
        Vector3 oppositeDirection = -attackDirection;

        // Instanciar el VFX de sangre en la dirección opuesta
        Instantiate(vfxHitEffect, vfxSpawn.transform.position, Quaternion.identity);
        Instantiate(vfxBlood, vfxSpawnBlood.transform.position, Quaternion.LookRotation(oppositeDirection));
        ApplyKnockback();

        foreach (var renderer in skinnedMeshRenderers)
        {
            StartCoroutine(BlinkEffect(renderer));
        }
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

    private IEnumerator BlinkEffect(SkinnedMeshRenderer renderer)
    {
        float endTime = Time.time + blinkDuration;
        while (Time.time < endTime)
        {
            float lerp = Mathf.Clamp01((endTime - Time.time) / blinkDuration);
            float intensity = (lerp * blinkIntensity) + 1;
            renderer.material.color = Color.white * intensity;
            yield return null;
        }
        renderer.material.color = Color.white;
    }
    public void StartBlinkEffects()
    {
        foreach (var renderer in skinnedMeshRenderers)
        {
            StartCoroutine(BlinkEffect(renderer));
        }
    }

    IEnumerator UpdateSliderOrientation()
    {
        while (true)
        {
            // Copia la rotación de la cámara, pero mantiene el `Slider` orientado horizontalmente respecto al suelo.
            Quaternion cameraRotation = Camera.main.transform.rotation;
            cameraRotation.x = 0; // Neutraliza la rotación en X
            cameraRotation.z = 0; // Neutraliza la rotación en Z

            sliderVida.transform.rotation = cameraRotation;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
