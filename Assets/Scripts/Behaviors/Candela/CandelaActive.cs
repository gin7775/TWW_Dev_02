using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandelaActive : MonoBehaviour
{
    public Animator start;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            start.SetTrigger("Idle");
        }
    }
}
