using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInfo : MonoBehaviour
{

    public int sceneIndex,spawnIndex,nextIndex,sceneMusic;
    
    public GameObject[] playerSpawn;
    private SoundManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<SoundManager>();
        GameManager.gameManager.playerSpawn = playerSpawn;
        spawnIndex = GameManager.gameManager.spawnIndex;

        GameManager.gameManager.SetPosition(playerSpawn[spawnIndex].transform);

        //audioManager.ChoseMusic(sceneMusic); Hasta utilizar el audio manager


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.tag == "Player")
        {
            GameManager.gameManager.spawnIndex = nextIndex;
            GameManager.gameManager.NextScene(sceneIndex);

        }
    }
}
