using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public SceneInfo SceneInfo;
    public int sceneIndexGameManager;
    public float transitionTime;
    public static GameManager gameManager { get; private set; }
    public GameObject[] playerSpawn;
    private GameObject player;
    public int spawnIndex;
    public Animator fader;
    public bool death;
    public bool newGame;
    public DataPersistenceManager dataPersistenceManager;

    void Start()
    {
        dataPersistenceManager = DataPersistenceManager.instance;
        death = false;
        transitionTime = 1f;
        fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        SceneInfo = FindObjectOfType<SceneInfo>();
    }

    private void Update()
    {
        if (SceneInfo == null)
        {
            try
            {
                SceneInfo = FindObjectOfType<SceneInfo>();
                playerSpawn = SceneInfo.playerSpawn;
            }
            catch (Exception)
            {

            }
        }
    }

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
    }


    public void GameStart()
    {
        NextScene(1);
    }

    public void NextScene(int scene)
    {

        if (fader == null)
        {
            fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<Animator>();

        }
        StartCoroutine(Loadlevel(scene));

    }
    public void SetPosition(Transform pos)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = pos.position;

    }
    public void Death()
    {
        gameManager.death = true;
        NextScene(sceneIndexGameManager);
    }

    IEnumerator Loadlevel(int index)
    {
        //SceneManager.LoadScene(index);

        fader.SetTrigger("Fade");

        yield return new WaitForSeconds(transitionTime);

        if (!death)
        {
            SceneManager.LoadScene(index);
        }
        if (death || newGame)
        {
            SceneManager.LoadScene(sceneIndexGameManager);
            newGame = false;
            dataPersistenceManager.LoadGame();
            SceneInfo.deathScene();
        }

        if (SceneManager.GetActiveScene().buildIndex == index)
        {
            playerSpawn = GameObject.FindGameObjectsWithTag("PlayerSpawn");


        }
        death = false;

    }
}



