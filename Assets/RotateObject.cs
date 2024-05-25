using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        // Rotar el objeto alrededor del eje Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
