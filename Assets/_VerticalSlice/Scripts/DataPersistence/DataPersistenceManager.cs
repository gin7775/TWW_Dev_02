using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening.Core.Easing;
using System.Data;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public string name;

    public static DataPersistenceManager instance { get; private set; }
    private void Awake()
    {

        //if (instance == null || instance == this)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this);

        //}
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}
        if (instance != null)
        {
            //Debug.LogError("More than one data persistence manager finding on the scene");
            
        }
        else if (gameData != null && gameData.death)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        name = "Nuevo DataPersistence";
        //if(instance != null)
        //{
        //    Debug.LogError("More than one data persistence manager finding on the scene");
        //}
        //instance = this;
        //DontDestroyOnLoad(this);

    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        if (instance.gameData == null || GameManager.gameManager.death || instance.gameData.newGame)
        {
            
            LoadGame();
        }
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        this.SaveGame();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        // TODO: Cargar cualquier dato desde un archivo de data handler
        if(this.gameData == null)
        {
            NewGame();
            Debug.Log("EStoy entrando a crear una nueva data del juego");
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
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    //private void OnApplicationQuit()
    //{
    //    SaveGame();
    //}

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
