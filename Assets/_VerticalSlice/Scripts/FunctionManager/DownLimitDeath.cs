using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DownLimitDeath : MonoBehaviour
{
    public GameObject player;
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        StartCoroutine(MirarDistancia());

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public IEnumerator MirarDistancia() 
    {
        if (player != null)
        {
            if (player.transform.position.y <= this.transform.position.y - 10) 
            {


                if (playerStats != null)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    Debug.Log("Has tocado");

                }

            }
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(MirarDistancia());

    }
}
