using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;
using UnityEngine.Playables;

public class SceneInfo : MonoBehaviour, IDataPersistence
{
    public bool endTest;
    public int sceneIndex, spawnIndex, nextIndex, controlPoint, sceneMusic, sceneSvaeActiva;
    //public int[] controlPoints;
    public DataPersistenceManager dataPersistenceManager;
    public GameObject[] playerSpawn;
    private SoundManager audioManager;
    public TextMeshPro TestText;

    void Start()
    {
        GameManager.gameManager.SetPosition(playerSpawn[GameManager.gameManager.spawnIndex].transform);

        audioManager = FindObjectOfType<SoundManager>();

    }

    void Update()
    {

    }



    public void deathScene()
    {
        nextIndex = controlPoint;
    }
    IEnumerator EndTest()
    {
        GameManager.gameManager.fader.SetTrigger("Fade");
        TestText.text = "Gracias por Jugar";
        yield return new WaitForSeconds(GameManager.gameManager.transitionTime + 3);
        Application.Quit();

    }

    public void LoadData(GameData gameData)
    {
        sceneIndex = gameData.sceneIndex;
        GameManager.gameManager.sceneIndexGameManager = sceneIndex;
        controlPoint = gameData.spawnPointIndex;
        GameManager.gameManager.spawnIndex = controlPoint;
        GameManager.gameManager.SetPosition(playerSpawn[controlPoint].transform);

    }

    public void SaveData(ref GameData data)
    {
        data.spawnPointIndex = GameManager.gameManager.spawnIndex;
        data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.gameManager.sceneIndexGameManager = data.sceneIndex;
        data.death = false;
        data.newGame = false;
        GameManager.gameManager.death = false;
    }
}
