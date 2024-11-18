using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int keysToCollect;
    public Animator openDoorAnim;
    public GameObject panel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Player>().collectedKeys >= keysToCollect)
            {
                openDoorAnim.SetTrigger("Active");
                //Destroy(this.gameObject, );
            }

            
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Player>().collectedKeys == 0)
            {

                panel.SetActive(true);
            }


        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Player>().collectedKeys == 0)
            {

                panel.SetActive(false);
            }


        }
    }
}
