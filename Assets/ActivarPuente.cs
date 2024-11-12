using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPuente : MonoBehaviour

{

    [SerializeField] Animator puenteAnim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            puenteAnim.SetTrigger("Action");
            MiFmod.Instance.PlayFondo("SoundTrack/Boss Theme");


        }
    }
}
