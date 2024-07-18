using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManagement : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public static DataManagement Instance { get; private set; }

    private List<IDataManagement> dataManagement;
    private GameData gameData;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("DataManagement Instance created");
        }

        Instance = this;

    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataManagement = FindAllDataManagementObject();
        if(FlagGame.Instance.IsLoadGame)
        {
            LoadGame();
            FlagGame.Instance.ResetFlag();
        } else
        {
            NewGame();
            FlagGame.Instance.ResetFlag();
        }
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load game data from file
        this.gameData = dataHandler.Load();

        // If no game data found, create new game data
        if (this.gameData == null)
        {
            Debug.Log("No game data found, creating new game data");
            NewGame();
        }

        // Load data from data management
        foreach (var data in dataManagement)
        {
            data.LoadData(this.gameData);
        }
    }

    public void SaveGame()
    {
        // pass the data to other scripts to they can update it
        foreach (var data in dataManagement)
        {
            data.SaveData(ref gameData);
        }

        // Save game data to file
        dataHandler.Save(gameData);
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataManagement> FindAllDataManagementObject()
    {
        IEnumerable<IDataManagement> dataManagement = FindObjectsOfType<MonoBehaviour>().OfType<IDataManagement>();

        return dataManagement.ToList();
    }
}
