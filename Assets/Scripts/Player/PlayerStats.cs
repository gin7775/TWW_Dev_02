using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;
using Cinemachine.PostFX;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
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

    

    public ParticleSystem healthParticle;
    //public GameObject healthSpawn;
    void Start()
    {
        healLV = 1;
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        UpdateHealCountUI();

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

       

        Debug.Log("Guardado esta" + PlayerPrefs.GetInt("Health") + "y tiene de vida" + currentHealth);
        healInstances = healLV;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
        currentHealth = Mathf.Max(currentHealth, 0);

        healthBar.UpdateMaskImage(currentHealth);
        anim.SetTrigger("Hurt");
        ControladorSonidos.Instance.EjecutarSonido(sfxHurt);
        PlayerPrefs.SetInt("Health",currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
           
            anim.SetTrigger("Death");
            PlayerPrefs.SetInt("firstHeal", 1);
            PlayerPrefs.DeleteKey("Health");
            PlayerPrefs.DeleteKey("firstHeal");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

    public void HealPlease()
    {
        if (healInstances > 0)
        {

            canMove = false; // Desactivar movimiento
            anim.SetTrigger("Health");
            StartCoroutine(EnableMovementAfterDelay(0.8f));
            ControladorSonidos.Instance.EjecutarSonido(sfxHeal);
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
}
