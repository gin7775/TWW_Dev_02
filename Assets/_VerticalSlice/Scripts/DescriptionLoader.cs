using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionLoader : MonoBehaviour
{
    private FileDataHandler dataHandler;
    private GameData gameData;
    public Button saveButton;
    public TextMeshProUGUI descriptionText;
    public int i;

    void Start()
    {
        try
        {
            dataHandler = new FileDataHandler(Application.persistentDataPath, $"SAVE_{i}.game", false); // TODO: Change false for true
            gameData = dataHandler.Load();
            FileInfo fileInfo = new FileInfo(Application.persistentDataPath + $"/SAVE_{i}.game");
            DateTime lastModified = fileInfo.LastWriteTime;
            descriptionText.text = $"CHAPTER {gameData.chapter}-{gameData.perCnt}% COMPLETED - {lastModified}";
        } catch (Exception)
        {
            saveButton.enabled = false;
        }
    }

    void Update()
    {

    }
}
