using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public SceneInfo SceneInfo;
    //Se usa para decirle al scene info cual es este punto 
    public int pointIndex;

    private void Start()
    {
        SceneInfo = FindObjectOfType<SceneInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        SceneInfo.controlPoint = pointIndex;
        Destroy(this.gameObject);
    }
}
