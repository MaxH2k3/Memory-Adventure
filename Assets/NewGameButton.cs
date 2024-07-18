using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    public int GameStartScene;

    public void StartGame()
    {
        DeleteSaveGame();
        SceneManager.LoadScene(GameStartScene);
        FlagGame.Instance.IsLoadGame = false;
    }

    public void DeleteSaveGame()
    {
        var dataDirPath = Application.persistentDataPath;
        var dataFileName = "data.game";
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

}
