using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class MenuController : MonoBehaviour
{
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    public void loadGameDialogYes()
    {
        if (IsGameSaved())
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    private bool IsGameSaved()
    {
        var dataDirPath = Application.persistentDataPath;
        var dataFileName = "data.game";
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(fullPath))
        {
            return true;
        }

        return false;
    }
}
