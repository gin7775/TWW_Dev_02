using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuenteFmod : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MiFmod.Instance.Play3D("3DSounds/Fuente", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
