using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
     public PlayerInput playerInput;
    public GameObject pauseMenuUI;
    public GameObject movementInput;
    public GameObject player;
  
    
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

        player.GetComponent<ComboAttackSystem>().enabled = true;
        playerInput.UI.Disable();
        playerInput.CharacterControls.Enable();
        playerInput.PlayerActions.Enable();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        
        GameIsPaused = true;
      
        movementInput.GetComponent<Input>().enabled = false;
        playerInput.UI.Enable();
        playerInput.CharacterControls.Disable();
        playerInput.PlayerActions.Disable();



    }




}
