using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneInfo : MonoBehaviour
{
    public bool endTest;
    public int sceneIndex,spawnIndex,nextIndex,controlPoint,sceneMusic;
    //public int[] controlPoints;
    public GameObject[] playerSpawn;
    private SoundManager audioManager;
    public TextMeshPro TestText;
    
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<SoundManager>();
        GameManager.gameManager.playerSpawn = playerSpawn;
        spawnIndex = GameManager.gameManager.spawnIndex;

        GameManager.gameManager.SetPosition(playerSpawn[spawnIndex].transform);

        //audioManager.ChoseMusic(sceneMusic); Hasta utilizar el audio manager
        TestText.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.tag == "Player")
        {
            if (endTest == false)
            {
                GameManager.gameManager.spawnIndex = nextIndex;
                GameManager.gameManager.NextScene(sceneIndex);
            }
            else
            {
                StartCoroutine(EndTest());
            }

        }
    }
    public void deathScene()
    {
        nextIndex = controlPoint;
        GameManager.gameManager.spawnIndex = nextIndex;
        GameManager.gameManager.NextScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator EndTest()
    {
        GameManager.gameManager.fader.SetTrigger("Fade");
        TestText.text = "Gracias por Jugar";
        yield return new WaitForSeconds(GameManager.gameManager.transitionTime+3);
        Application.Quit();

    }
}
