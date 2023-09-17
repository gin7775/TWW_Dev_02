using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UiMenuInicial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoadSceneByName(string Bosque3)
    {
        // Carga la escena con el nombre especificado
        SceneManager.LoadScene(Bosque3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
