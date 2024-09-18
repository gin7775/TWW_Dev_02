using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrezeableObject : MonoBehaviour
{
    private bool isFrozen = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Freeze(float freezeDuration)
    {
        if (isFrozen) return;  // No congelar si ya está congelado
        //anim.SetBool("Freeze", true);
        isFrozen = true;
        
        Debug.Log("Objeto congelado por " + freezeDuration + " segundos.");

        StartCoroutine(Unfreeze(freezeDuration));
    }

    private IEnumerator Unfreeze(float freezeDuration)
    {
        yield return new WaitForSeconds(freezeDuration);
        isFrozen = false;
       // anim.SetBool("Freeze", false);
        Debug.Log("Objeto descongelado.");
    }
}
