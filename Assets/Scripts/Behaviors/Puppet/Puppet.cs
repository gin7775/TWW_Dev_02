using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Puppet : MonoBehaviour, IEnemyFreezable
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

    ContenedorPuppet contenedorPuppet;
    NavMeshAgent agent;
    public GameObject healthBarCanvas;

    public Slider sliderVida;
    public List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
    [SerializeField] private AudioClip sfxGolpe;
    public Rigidbody puppetRigidbody;
    public float knockbackDistance = 1.5f;
    public float knockbackDuration = 0.3f; // Duración del retroceso
    public float angulo;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;
    Animator animPuppet;
    private bool isFrozen = false;
    public bool isFreezable = true;

    public bool IsFreezable => isFreezable;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animPuppet = GetComponent<Animator>();
        playerReference = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(UpdateSliderOrientation());
        cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
        DOTween.Init();
        contenedorPuppet = this.GetComponent<ContenedorPuppet>();
        player = playerReference.transform;
    }

    // Update is called once per frame
    void Update()
    {
        sliderVida.value = currentHealth;
       

        angulo = this.transform.localEulerAngles.y;

    }
    // Método para Congelar al enemigo
    public void Freeze(float freezeDuration)
    {
        if (!isFreezable || isFrozen) return;  // No congelar si no es congelable o ya está congelado

        isFrozen = true;
        agent.speed = 0f;
        // Congelar el estado actual del Animator
        AnimatorStateInfo currentAnimatorState = contenedorPuppet.animPuppet.GetCurrentAnimatorStateInfo(0); // Capturamos el estado actual
        contenedorPuppet.animPuppet.Play(currentAnimatorState.fullPathHash, -1, currentAnimatorState.normalizedTime); // Forzamos a reproducir el estado actual en el frame que está
        contenedorPuppet.animPuppet.speed = 0f;  // Pausar la animación completamente
        

        // Iniciar el temporizador para descongelar
        StartCoroutine(Unfreeze(freezeDuration));
    }

    // Método para descongelar al enemigo
    private IEnumerator Unfreeze(float freezeDuration)
    {
        yield return new WaitForSeconds(freezeDuration);
        agent.speed = 2;
        // Reactivar la animación ajustando la velocidad de vuelta a la normalidad
        contenedorPuppet.animPuppet.speed = 1f;
        // Restablecer la velocidad del agente a su valor original

        isFrozen = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        foreach (var renderer in skinnedMeshRenderers)
        {
            StartCoroutine(BlinkEffect(renderer));
        }
        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(FrameFreeze(0.1f));
        MiFmod.Instance.Play("SFX_2d/HeridoPuppet");

        Vector3 attackDirection = (player.position - transform.position).normalized;
       Vector3 oppositeDirection = -attackDirection;

    // Instanciar el VFX de sangre en la dirección opuesta
      Instantiate(vfxHitEffect, vfxSpawn.transform.position, Quaternion.identity);
      //Instantiate(vfxBlood, vfxSpawnBlood.transform.position, Quaternion.LookRotation(oppositeDirection));
        ApplyKnockback();

        contenedorPuppet.animPuppet.SetTrigger("Hit");
        animPuppet.SetTrigger("Navigation");
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
    private void ApplyKnockback()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 knockbackPosition = transform.position - directionToPlayer * knockbackDistance;

        // Animar el movimiento de retroceso
        transform.DOMove(knockbackPosition, knockbackDuration).SetEase(Ease.OutExpo); 
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
