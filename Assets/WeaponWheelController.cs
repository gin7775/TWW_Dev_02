using UnityEngine;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    public bool weaponWheelSelected = false;  // Controla si el Weapon Wheel está activo

    public GameObject[] weaponIcons;  // Array de GameObjects para los íconos de las armas
    public int weaponID = 1;  // El ID actual del arma seleccionada (1 por defecto para el proyectil básico)

    private WeaponWheelButton currentSelectedButton;  // Referencia al botón actualmente seleccionado
    public SpellScript spellScript;  // Referencia al script de disparo

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
    }

    // Método para actualizar los íconos según el weaponID
    private void UpdateWeaponIcon()
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
        // Solo permite la selección si el Weapon Wheel está abierto
        if (!weaponWheelSelected)
        {
            Debug.Log("Weapon Wheel no está abierto, no se puede cambiar de ítem.");
            return;
        }

        // Si el botón actual ya está seleccionado y el jugador hace clic nuevamente, lo deselecciona
        if (currentSelectedButton == button)
        {
            button.Deselected();
            currentSelectedButton = null;
            weaponID = 1;  // Restablece el weaponID al proyectil básico (1)
            spellScript.SetProjectile(weaponID);  // Restablece al proyectil por defecto
            UpdateWeaponIcon();  // Actualiza los íconos
        }
        else
        {
            // Si hay otro botón seleccionado, lo deselecciona
            if (currentSelectedButton != null)
            {
                currentSelectedButton.Deselected();
            }

            // Selecciona el nuevo botón
            currentSelectedButton = button;
            currentSelectedButton.Selected();
            weaponID = button.ID;  // Actualiza el ID del arma seleccionada
            spellScript.SetProjectile(weaponID);  // Cambia el proyectil en el SpellScript
            UpdateWeaponIcon();  // Actualiza los íconos
        }
    }
}
