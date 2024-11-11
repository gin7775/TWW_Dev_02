using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
     public PlayerInput playerInput;
    public GameObject pauseMenuUI;
    public GameObject movementInput;
    public GameObject player;

    public GameObject firstGameObjectMenu;
    void Start()
    {

        playerInput  = new PlayerInput();
       
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        movementInput.GetComponent<Input>().enabled = true;

        player.GetComponent<ComboAttackSystem>().isOnMenu = false;
       
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstGameObjectMenu);
        GameIsPaused = true;
      
        movementInput.GetComponent<Input>().enabled = false;

        player.GetComponent<ComboAttackSystem>().isOnMenu = true;


    }




}
