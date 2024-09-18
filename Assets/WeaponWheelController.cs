using UnityEngine;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    public bool weaponWheelSelected = false;  // Controla si el Weapon Wheel est� activo

    public GameObject[] weaponIcons;  // Array de GameObjects para los �conos de las armas
    public int weaponID = 1;  // El ID actual del arma seleccionada (1 por defecto para el proyectil b�sico)

    private WeaponWheelButton currentSelectedButton;  // Referencia al bot�n actualmente seleccionado
    public SpellScript spellScript;  // Referencia al script de disparo

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
    }

    // M�todo para actualizar los �conos seg�n el weaponID
    private void UpdateWeaponIcon()
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
        // Solo permite la selecci�n si el Weapon Wheel est� abierto
        if (!weaponWheelSelected)
        {
            Debug.Log("Weapon Wheel no est� abierto, no se puede cambiar de �tem.");
            return;
        }

        // Si el bot�n actual ya est� seleccionado y el jugador hace clic nuevamente, lo deselecciona
        if (currentSelectedButton == button)
        {
            button.Deselected();
            currentSelectedButton = null;
            weaponID = 1;  // Restablece el weaponID al proyectil b�sico (1)
            spellScript.SetProjectile(weaponID);  // Restablece al proyectil por defecto
            UpdateWeaponIcon();  // Actualiza los �conos
        }
        else
        {
            // Si hay otro bot�n seleccionado, lo deselecciona
            if (currentSelectedButton != null)
            {
                currentSelectedButton.Deselected();
            }

            // Selecciona el nuevo bot�n
            currentSelectedButton = button;
            currentSelectedButton.Selected();
            weaponID = button.ID;  // Actualiza el ID del arma seleccionada
            spellScript.SetProjectile(weaponID);  // Cambia el proyectil en el SpellScript
            UpdateWeaponIcon();  // Actualiza los �conos
        }
    }
}
