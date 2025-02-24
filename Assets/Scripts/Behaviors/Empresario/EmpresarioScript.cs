using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmpresarioScript : MonoBehaviour, IEnemyFreezable
{
    public Transform player;
    public GameObject holder, playerReference;
    public float waitingTime;
    //HIELO
    public bool IsFreezable => isFreezable;
    public bool isFreezable = true;
    private bool isFrozen = false;
    //HIELO 
    public float currentHealth;

    public GameObject vfxHitEffect;

    public GameObject vfxHitEffectFinish;

    public GameObject vfxSpawn;
    
    public GameObject healthBarCanvas;
    
    private CinemachineImpulseSource cinemachineImpulseSource;

    [SerializeField] private AudioClip sfxHit;

    ContenedorEmpresario contenedorEmpresario;
    public float angulo;
    public Slider sliderVida;
    public float knockbackDistance = 1.5f;
    public float knockbackDuration = 0.3f; // Duraci�n del retroceso

    public List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

    public float blinkIntensity;
    public float blinkDuration;
    // Start is called before the first frame update
    void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        DOTween.Init();
        cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
        StartCoroutine(UpdateSliderOrientation());
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

    public void Freeze(float freezeDuration)
    {
        if (!isFreezable || isFrozen) return;

        isFrozen = true;
        Debug.Log("Se congela empresario");
        StartCoroutine(Unfreeze(freezeDuration));
    }

    private IEnumerator Unfreeze(float freezeDuration)
    {
        yield return new WaitForSeconds(freezeDuration);
        Debug.Log("Se Descongela empresario");
        isFrozen = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(FrameFreeze(0.1f));
        foreach (var renderer in skinnedMeshRenderers)
        {
            StartCoroutine(BlinkEffect(renderer));
        }

        MiFmod.Instance.Play("SFX_2d/Herido");
       
       

        // Instanciar el VFX de sangre en la direcci�n opuesta
        Instantiate(vfxHitEffect, vfxSpawn.transform.position, Quaternion.identity);
       
        ApplyKnockback();
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
            // Copia la rotaci�n de la c�mara, pero mantiene el `Slider` orientado horizontalmente respecto al suelo.
            Quaternion cameraRotation = Camera.main.transform.rotation;
            cameraRotation.x = 0; // Neutraliza la rotaci�n en X
            cameraRotation.z = 0; // Neutraliza la rotaci�n en Z

            sliderVida.transform.rotation = cameraRotation;

            yield return new WaitForSeconds(0.01f);
        }
    }

}
