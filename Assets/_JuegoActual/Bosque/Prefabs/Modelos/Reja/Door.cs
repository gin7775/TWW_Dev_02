using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int keysToCollect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Player>().collectedKeys >= keysToCollect)
            {
                Debug.Log("Abrete sezamo");
                Destroy(this.gameObject);
            }
        }
    }
}
