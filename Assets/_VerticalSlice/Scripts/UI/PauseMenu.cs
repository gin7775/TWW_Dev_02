using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public PlayerInput playerInput;
    public GameObject pauseMenuUI;
    //private GameObject player;

    public void Resume()
    {
        //playerInput.SwitchCurrentActionMap("Player");
        //player = GameObject.Find("_PlayerController").GetComponent<GameObject>();
        
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }

    public void Pause()
    {
        //playerInput.SwitchCurrentActionMap("UI");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        //player = GameObject.Find("_PlayerController").GetComponent<GameObject>();
        //player.SetActive(false);
        GameIsPaused = true;
    }


    
 
}
