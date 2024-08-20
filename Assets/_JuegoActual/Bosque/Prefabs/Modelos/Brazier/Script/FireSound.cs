using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MiFmod.Instance.Play3D("3DSounds/Fire", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
