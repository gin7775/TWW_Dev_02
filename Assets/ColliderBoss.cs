using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBoss : MonoBehaviour
{
    public bool noLock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            noLock = true;
        }
    }
}
