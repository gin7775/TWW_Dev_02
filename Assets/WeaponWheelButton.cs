using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class WeaponWheelButton : MonoBehaviour
{
    public int ID;  // El ID del �tem que representa este bot�n
    public WeaponWheelController controller;  // Referencia al WeaponWheelController

    private Animator anim;
    public string itemName;
    public TextMeshProUGUI itemText;
    public GameObject icon;
    private bool selected = false;

    // Llamado cuando se hace clic en el bot�n
    public void OnClick()
    {
        // Llama al controlador para manejar la selecci�n del bot�n
        controller.SelectButton(this);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // M�todo para seleccionar este bot�n
    public void Selected()
    {
        selected = true;
       // icon.SetActive(true);  // Muestra el �cono si est� seleccionado
        itemText.text = itemName;  // Muestra el nombre del �tem si est� seleccionado
    }

    // M�todo para deseleccionar este bot�n (solo se llama cuando se selecciona otro bot�n o el mismo bot�n)
    public void Deselected()
    {
        selected = false;
        //icon.SetActive(false);  // Oculta el �cono si no est� seleccionado
        itemText.text = "";  // Limpia el texto si no est� seleccionado
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        itemText.text = itemName;  // Muestra el nombre del �tem al hacer hover
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        itemText.text = "";  // Limpia el texto cuando se deja de hacer hover
    }
}
