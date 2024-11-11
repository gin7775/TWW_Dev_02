using UnityEngine;
using UnityEngine.UI; // Aseg�rate de incluir esto

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    public bool weaponWheelSelected = false;  // Controla si el Weapon Wheel est� activo

    public GameObject[] weaponIcons;  // Array de GameObjects para los �conos de las armas
    public Image[] cooldownImages;    // Array para las im�genes de cooldown de cada arma (relleno radial)
    public int weaponID = 1;  // El ID actual del arma seleccionada (1 por defecto para el proyectil b�sico)
    private int currentProjectileIndex;
    private WeaponWheelButton currentSelectedButton;  // Referencia al bot�n actualmente seleccionado
    public SpellScript spellScript;  // Referencia al script de disparo
    public bool blueProyectileActive = false;
    void Start()
    {
        // Asegurar que el �cono del proyectil predeterminado (ID 1) est� activo al inicio
        weaponID = 1;  // Proyectil predeterminado es el primero
        spellScript.SetProjectile(weaponID);  // Configura el proyectil predeterminado
        UpdateWeaponIcon();  // Asegura que el �cono correcto se muestra en la UI
    }

    void Update()
    {
        // Controlar la apertura del Weapon Wheel basado en la selecci�n
        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        // Actualizar los cooldowns visuales
        UpdateCooldownVisuals();
    }

    // M�todo para actualizar los �conos seg�n el weaponID
    public void UpdateWeaponIcon()
    {
        // Desactivar todos los �conos
        for (int i = 0; i < weaponIcons.Length; i++)
        {
            weaponIcons[i].SetActive(false);
        }

        // Activar solo el �cono correspondiente al weaponID, si el ID es v�lido
        if (weaponID > 0 && weaponID <= weaponIcons.Length)
        {
            weaponIcons[weaponID - 1].SetActive(true);  // Activar el �cono correspondiente
        }
    }

    // M�todo para manejar la selecci�n de un bot�n
    public void SelectButton(WeaponWheelButton button)
    {
        if (!weaponWheelSelected) return;
        if (!spellScript.ProjectilesUnlocked && button.ID > 1)
        {
            Debug.Log("Los proyectiles especiales a�n no est�n desbloqueados.");
            return;
        }

        if (currentSelectedButton == button )
        {
            button.Deselected();
            currentSelectedButton = null;
            weaponID = 1;
            spellScript.SetProjectile(weaponID);
            UpdateWeaponIcon();
        }
        else
        {
            if (currentSelectedButton != null) currentSelectedButton.Deselected();

            currentSelectedButton = button;
            currentSelectedButton.Selected();
            weaponID = button.ID;
            spellScript.SetProjectile(weaponID); // Cambia el proyectil usando el nuevo m�todo
            UpdateWeaponIcon();
        }
    }

    // M�todo para actualizar los visuales de cooldown en cada arma
    private void UpdateCooldownVisuals()
    {
        for (int i = 0; i < cooldownImages.Length; i++)
        {
            if (i < spellScript.projectileCooldownTimers.Length)
            {
                // Asume que el cooldown es 0 cuando est� disponible (complete el c�rculo)
                float cooldown = spellScript.projectileCooldownTimers[i];
                float maxCooldown = spellScript.projectileCooldownTimes[i];

                // Calcular el fillAmount basado en el progreso del cooldown
                cooldownImages[i].fillAmount = 1 - Mathf.Clamp01(cooldown / maxCooldown);
            }
        }
    }
}
