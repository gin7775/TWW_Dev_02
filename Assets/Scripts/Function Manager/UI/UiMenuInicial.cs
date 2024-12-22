using DG.Tweening.Plugins.Core.PathCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UiMenuInicial : MonoBehaviour, IDataPersistence
{
    public int sceneIndex;
    public int controlPoint;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void LoadSceneByName(string Bosque3)
    {
        // Carga la escena con el nombre especificado
        SceneManager.LoadScene(Bosque3);
    }

    public void LoadSceneByIndex()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ContinueGame()
    {
        List<DateTime> filesDates = new List<DateTime>(); List<int> positions = new List<int>(); 
        for (int i = 1; i < 5; i++)
        {
            FileInfo fileInfo = new FileInfo(Application.persistentDataPath + $"/SAVE_{i}.game"); 
            if (fileInfo.Exists) 
            { 
                filesDates.Add(fileInfo.LastWriteTime); 
                positions.Add(i); 
            } 
        } 
        if (filesDates.Count == 0) 
        { 
            Debug.LogError("No se encontraron archivos de guardado."); 
            return; 
        } 
        DateTime maxDate = filesDates[0]; 
        int maxPosition = positions[0]; 
        for (int i = 1; i < filesDates.Count; i++) 
        { 
            if (filesDates[i] > maxDate) 
            { 
                maxDate = filesDates[i]; 
                maxPosition = positions[i]; 
            } 
        } 
        DataPersistenceManager.instance.UpdateFileName(maxPosition);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadData(GameData gameData)
    {
        sceneIndex = gameData.sceneIndex;
        controlPoint = gameData.spawnPointIndex;
        GameManager.gameManager.spawnIndex = controlPoint;
    }

    public void SaveData(ref GameData data)
    {
        data.sceneIndex = sceneIndex;
    }
}
