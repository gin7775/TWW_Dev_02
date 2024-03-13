using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public SceneInfo SceneInfo;
    public float transitionTime;
    public static GameManager gameManager;
    public GameObject[] playerSpawn;
    private GameObject player;
    public int spawnIndex;
    private Animator fader;

    void Start()
    {
        transitionTime = 1f;
        fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        spawnIndex = 0;
        SceneInfo = FindObjectOfType<SceneInfo>();
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

        if(fader == null)
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
        SceneInfo.deathScene();
    }

    IEnumerator Loadlevel(int index)
    {

        fader.SetTrigger("Fade");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);

        if (SceneManager.GetActiveScene().buildIndex == index)
        {
            playerSpawn = GameObject.FindGameObjectsWithTag("PlayerSpawn");



        }

    }
}

    

