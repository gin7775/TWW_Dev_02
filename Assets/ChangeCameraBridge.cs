using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraBridge : MonoBehaviour
{
    private Camera mainCamera;

    public bool isOnTrigger;
    void Start()
    {
        // Obtener la cámara principal
        mainCamera = Camera.main;

        isOnTrigger = true;
    }

    // public void SwitchToPerspective()
    //{
    //    if (mainCamera != null)
    //    {
    //        mainCamera.orthographic = false;
    //    }
    //}
   
   

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
            
    //        Camera.main.ResetProjectionMatrix();
    //        Debug.Log(mainCamera.orthographic);

    //    }


    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        mainCamera.orthographic = true;


    //    }
    //}
}
