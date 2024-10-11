using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;
using Cinemachine.PostFX;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour, IDataPersistence
{
    public SceneInfo thisScene;
    public int healthLevel = 10;

    public int atFirst = 1;
    public bool healLVReset;

    public int maxHealth;
    [Range(0f,100f)]
    public int currentHealth;

    public int healInstances,healLV;
    public int enemyCount;

    public HealthBar healthBar;

    public CinemachineVolumeSettings[] volumeSettings;
    [SerializeField] private AudioClip sfxHeal;
    [SerializeField] private AudioClip sfxHurt;
    private Animator anim;

    public Text healCountText;

    public bool canMove = true;

    public bool invensible;
    private const float InvincibilityDuration = 0.5f;
    public ParticleSystem healthParticle;
    Player player;
    private CinemachineImpulseSource cinemachineImpulseSource;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;
   
    public List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

    //public GameObject healthSpawn;
    void Start()
    {
        invensible = false;
        player = GetComponent<Player>();
        cinemachineImpulseSource = this.GetComponent<CinemachineImpulseSource>();
        healLV = 1;
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        UpdateHealCountUI();
        thisScene = GetComponent<SceneInfo>();
        maxHealth = SetMaxHealthFromHealthLevel();
        if (PlayerPrefs.HasKey("HealLV"))
        {
            healLV = PlayerPrefs.GetInt("HealLV");
            Debug.Log("Tiene de instancias" + PlayerPrefs.GetInt("HealLV"));
        }

        currentHealth = PlayerPrefs.GetInt("Health");
        atFirst = PlayerPrefs.GetInt("firstHeal");
        if(atFirst == 1)
        {
            if(currentHealth != maxHealth)
            {
                currentHealth = maxHealth;
                //resetHeal();

                atFirst = 0;
                PlayerPrefs.SetInt("firstHeal", atFirst);

                Debug.Log("At first es" + PlayerPrefs.GetInt("firstHeal"));
            }
        }
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            
            PlayerPrefs.SetInt("Health", currentHealth);

        }
        if (healLVReset ==  true)
        {
            //Debug.Log("Recetea");
            resetHeal();
            enemyCount = 0;
            PlayerPrefs.SetInt("EnemyCount", enemyCount);
        }

        healthBar.UpdateMaskImage(100);

       

        //Debug.Log("Guardado esta" + PlayerPrefs.GetInt("Health") + "y tiene de vida" + currentHealth);
        healInstances = healLV;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void StartBlinkEffects()
    {
        foreach (var renderer in skinnedMeshRenderers)
        {
            StartCoroutine(BlinkEffect(renderer));
        }
    }
    // Update is called once per frame
    void Update()
    {
       
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

    private int SetMaxHealthFromHealthLevel()
    {

        maxHealth = healthLevel * 10;

        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!invensible && !player.isDodging)
        {
            currentHealth -= damage;
            foreach (var renderer in skinnedMeshRenderers)
            {
                StartCoroutine(BlinkEffect(renderer));
            }
            currentHealth = Mathf.Max(currentHealth, 0);
            healthBar.UpdateMaskImage(currentHealth);
            anim.SetTrigger("Hurt");
            MiFmod.Instance.Play("SFX_2d/Herido");
            cinemachineImpulseSource.GenerateImpulse();
            PlayerPrefs.SetInt("Health", currentHealth);
            blinkTimer = blinkDuration;
            if (currentHealth <= 0)
            {
                currentHealth = 0;

                anim.SetTrigger("Death");
                PlayerPrefs.SetInt("firstHeal", 1);
                PlayerPrefs.DeleteKey("Health");
                PlayerPrefs.DeleteKey("firstHeal");
                GameManager.gameManager.Death();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                StartCoroutine(BecomeInvincibleForTime(InvincibilityDuration));
            }
        }
    }

    
    public IEnumerator BecomeInvincibleForTime(float time)
    {
        invensible = true;
        yield return new WaitForSeconds(time);
        invensible = false;
    }

    public void HealPlease()
    {
        if (healInstances > 0)
        {

            canMove = false; // Desactivar movimiento
            anim.SetTrigger("Health");
            StartCoroutine(EnableMovementAfterDelay(0.8f));
            MiFmod.Instance.Play("SFX_2d/Curar");
            healthParticle.Play();
            currentHealth += 50;
            healInstances--;
            healthBar.UpdateMaskImage(currentHealth);
            UpdateHealCountUI();
            PlayerPrefs.SetInt("Health", currentHealth);
        }
       

        Debug.Log("Curaria");
    }

    private IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true; // Reactivar movimiento
    }

    private void UpdateHealCountUI()
    {
        if (healCountText != null)
        {
            healCountText.text = " " + healInstances;
        }
    }

    public void increaseHeal()
    {
        healLV++;
        healInstances++;
        UpdateHealCountUI();
        PlayerPrefs.SetInt("HealLV",healLV);
        Debug.Log("Tiene de instancias" + PlayerPrefs.GetInt("HealLV"));

    }

    public void IncreaseEnemyCount()
    {
        enemyCount++;
        PlayerPrefs.SetInt("EnemyCount", enemyCount);
    }
    public void resetHeal()
    {

        healLV = 1;
        healInstances = healLV;
        PlayerPrefs.SetInt("HealLV", healLV);
        Debug.Log("Tiene de instancias" + PlayerPrefs.GetInt("HealLV"));


    }

    public void LoadData(GameData gameData)
    {
        this.currentHealth = gameData.currentHealth;
    }

    public void SaveData(ref GameData data)
    {
        data.currentHealth = this.currentHealth;
    }
}
