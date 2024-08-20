using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic_1 : MonoBehaviour
{
    public float videoTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(videoTime);
        GameManager.gameManager.NextScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
