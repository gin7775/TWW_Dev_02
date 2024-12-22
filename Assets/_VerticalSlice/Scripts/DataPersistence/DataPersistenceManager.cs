using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening.Core.Easing;
using System.Data;
using Michsky.UI.Dark;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    //public bool needLoad;
    private GameData gameData;
    private bool isNewGame;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //name = "Nuevo DataPersistence";

    }

    private void Start()
    {
        //needLoad = false;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        if (GameManager.gameManager.death || instance.gameData.newGame)
        {
            LoadGame();
        }
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        this.isNewGame = true;
        this.SaveGame();
    }

    public void UpdateFileName(int i)
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, $"SAVE_{i}.game", useEncryption);
    }

    public void LoadGame()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        this.gameData = dataHandler.Load();
        // TODO: Cargar cualquier dato desde un archivo de data handler
        if(this.gameData == null)
        {
            //NewGame();
            Debug.Log("Primera llamada");
        }

        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.LoadData(gameData);

        }
        // TODO: Enviar la data a todos los scripts necesarios
    }
    public void SaveGame()
    {
        Debug.Log("Guarda");
        if (this.isNewGame)
        {
            this.isNewGame = false;
            dataHandler.CheckAndCreateSaveFile(ref gameData);

        }
        
        else
        {
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
            {
                dataPersistence.SaveData(ref gameData);
            }
            dataHandler.Save(gameData);
        }
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        List<IDataPersistence> dataPersistenceObjects = new List<IDataPersistence>();

        foreach (var monoBehaviour in FindObjectsOfType<MonoBehaviour>())
        {
            if (monoBehaviour is IDataPersistence dataPersistence)
            {
                dataPersistenceObjects.Add(dataPersistence);
            }
        }

        return dataPersistenceObjects;

    }

}
