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
        //SceneInfo = FindObjectOfType<SceneInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneInfo.controlPoint = pointIndex;
            Debug.Log("El punto de control es " + SceneInfo.controlPoint);
            Destroy(this.gameObject, 1.3f);
        }
        
    }
}
