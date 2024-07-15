using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    public static DataManagement Instance { get; private set; }

    private GameData gameData;

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
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if(this.gameData == null)
        {
            this.gameData = new GameData();

            NewGame();
        }
    }

    public void SaveGame()
    {

    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }
}
