using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopOutController : MonoBehaviour
{
    public int MenuScene;

    public void GoToMainMenu()
    {
        GameManager.Instance.ResumeGame();
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene(MenuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
