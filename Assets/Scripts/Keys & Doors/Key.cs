using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool keyLock;
   // public Animator animator;

    private void Start()
    {
        keyLock = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && keyLock == false)
        {
            keyLock = true;
            other.gameObject.GetComponent<Player>().collectedKeys++;
            Destroy(this.gameObject);
            MiFmod.Instance.Play("SFX_2d/Key");
            //animator.SetTrigger("Active");
        }
    }
}
