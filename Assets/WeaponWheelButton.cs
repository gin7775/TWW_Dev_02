using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class WeaponWheelButton : MonoBehaviour
{
    public int ID;  // El ID del ítem que representa este botón
    public WeaponWheelController controller;  // Referencia al WeaponWheelController

    private Animator anim;
    public string itemName;
    public TextMeshProUGUI itemText;
    public GameObject icon;
    private bool selected = false;

    // Llamado cuando se hace clic en el botón
    public void OnClick()
    {
        // Llama al controlador para manejar la selección del botón
        controller.SelectButton(this);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Método para seleccionar este botón
    public void Selected()
    {
        selected = true;
       // icon.SetActive(true);  // Muestra el ícono si está seleccionado
        itemText.text = itemName;  // Muestra el nombre del ítem si está seleccionado
    }

    // Método para deseleccionar este botón (solo se llama cuando se selecciona otro botón o el mismo botón)
    public void Deselected()
    {
        selected = false;
        //icon.SetActive(false);  // Oculta el ícono si no está seleccionado
        itemText.text = "";  // Limpia el texto si no está seleccionado
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        itemText.text = itemName;  // Muestra el nombre del ítem al hacer hover
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        itemText.text = "";  // Limpia el texto cuando se deja de hacer hover
    }
}
