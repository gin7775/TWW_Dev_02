using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir esto

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    public bool weaponWheelSelected = false;  // Controla si el Weapon Wheel está activo

    public GameObject[] weaponIcons;  // Array de GameObjects para los íconos de las armas
    public Image[] cooldownImages;    // Array para las imágenes de cooldown de cada arma (relleno radial)
    public int weaponID = 1;  // El ID actual del arma seleccionada (1 por defecto para el proyectil básico)
    private int currentProjectileIndex;
    private WeaponWheelButton currentSelectedButton;  // Referencia al botón actualmente seleccionado
    public SpellScript spellScript;  // Referencia al script de disparo
    public bool blueProyectileActive = false;
    void Start()
    {
        // Asegurar que el ícono del proyectil predeterminado (ID 1) esté activo al inicio
        weaponID = 1;  // Proyectil predeterminado es el primero
        spellScript.SetProjectile(weaponID);  // Configura el proyectil predeterminado
        UpdateWeaponIcon();  // Asegura que el ícono correcto se muestra en la UI
    }

    void Update()
    {
        // Controlar la apertura del Weapon Wheel basado en la selección
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

    // Método para actualizar los íconos según el weaponID
    public void UpdateWeaponIcon()
    {
        // Desactivar todos los íconos
        for (int i = 0; i < weaponIcons.Length; i++)
        {
            weaponIcons[i].SetActive(false);
        }

        // Activar solo el ícono correspondiente al weaponID, si el ID es válido
        if (weaponID > 0 && weaponID <= weaponIcons.Length)
        {
            weaponIcons[weaponID - 1].SetActive(true);  // Activar el ícono correspondiente
        }
    }

    // Método para manejar la selección de un botón
    public void SelectButton(WeaponWheelButton button)
    {
        if (!weaponWheelSelected) return;
        if (!spellScript.ProjectilesUnlocked && button.ID > 1)
        {
            Debug.Log("Los proyectiles especiales aún no están desbloqueados.");
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
            spellScript.SetProjectile(weaponID); // Cambia el proyectil usando el nuevo método
            UpdateWeaponIcon();
        }
    }

    // Método para actualizar los visuales de cooldown en cada arma
    private void UpdateCooldownVisuals()
    {
        for (int i = 0; i < cooldownImages.Length; i++)
        {
            if (i < spellScript.projectileCooldownTimers.Length)
            {
                // Asume que el cooldown es 0 cuando está disponible (complete el círculo)
                float cooldown = spellScript.projectileCooldownTimers[i];
                float maxCooldown = spellScript.projectileCooldownTimes[i];

                // Calcular el fillAmount basado en el progreso del cooldown
                cooldownImages[i].fillAmount = 1 - Mathf.Clamp01(cooldown / maxCooldown);
            }
        }
    }
}
