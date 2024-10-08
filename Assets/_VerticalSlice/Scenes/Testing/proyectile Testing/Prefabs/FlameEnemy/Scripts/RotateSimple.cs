using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSimple : MonoBehaviour
{
    private Vector3 rotationAngle;
    public float speed;

    private void Start()
    {
        rotationAngle = new Vector3(0, 1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotationAngle, speed);       
    }
}
