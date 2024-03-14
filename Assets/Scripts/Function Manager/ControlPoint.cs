using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public bool canTriger;
    public SceneInfo SceneInfo;
    //Se usa para decirle al scene info cual es este punto 
    public int pointIndex;

    private void Start()
    {
        canTriger = true;
        //SceneInfo = FindObjectOfType<SceneInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& canTriger == true)
        {
            SceneInfo.controlPoint = pointIndex;
            Debug.Log("El punto de control es " + SceneInfo.controlPoint);
            canTriger=false;
        }
        
    }
}
