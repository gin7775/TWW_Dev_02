
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    public bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    Player player;
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            
                anim.SetBool("OpenWeaponWheel", false);
            
        }

        switch(weaponID)
        {
            case 0:
                selectedItem.sprite = noImage;
                break;
            case 1:
                Debug.Log("Poderes");
                break;
            case 2:
                Debug.Log("Poderes");
                break;
            case 3:
                Debug.Log("Poderes");
                break;
            case 4:
                Debug.Log("Poderes");
                break;
            case 5:
                Debug.Log("Poderes");
                break;
            case 6:
                Debug.Log("Poderes");
                break;
        }
    }



}
