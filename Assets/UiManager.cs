using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public GameObject[] objectsToHide;
    public bool inicio;
    public void Start()
    {
        if (inicio)
        {
            Debug.Log("Time is paused");
            Time.timeScale = 0f;
        }
        else
        {
            GameStart();
        }
    }

    public void GameStart() 
    {
        Time.timeScale = 1f;
        for (int i = 0; i < objectsToHide.Length;i++) 
        {

            objectsToHide[i].SetActive(false);
            
        }
        
    }

}
