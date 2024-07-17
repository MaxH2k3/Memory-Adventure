using Assets.Scripts.Enums;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, IDataManagement
{
    [SerializeField]
    private GameState currentState;
    private GameState previousState;

    [Header("UI")]
    [SerializeField]
    private GameObject pauseScreen;

    [SerializeField]
    public GameObject ResumeButton;

    [SerializeField]
    public GameObject RestartButton;

    private new void Awake()
    {
        base.Awake();

        DisableScreen();

        ChangeState(GameState.Gameplay);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void OnPlayerDeath()
    {
        ResumeButton.SetActive(false);
        RestartButton.SetActive(true);

        ChangeState(GameState.Paused);
        pauseScreen.SetActive(true);
        PlayerController.Instance.canMove = false;
    }

    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            Time.timeScale = 0f;
            previousState = currentState;
            ChangeState(GameState.Paused);
            pauseScreen.SetActive(true);
            PlayerController.Instance.canMove = false;
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            Time.timeScale = 1f;
            ChangeState(previousState);
            pauseScreen.SetActive(false);
            PlayerController.Instance.canMove = true;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started && (currentState == GameState.Paused || currentState == GameState.Gameplay))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void DisableScreen()
    {
        pauseScreen.SetActive(false);
    }

    public void LoadData(GameData gameData)
    {
        if(SceneManager.GetActiveScene().buildIndex != gameData.Scence)
        {
            SceneManager.LoadScene(gameData.Scence);
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.Scence = SceneManager.GetActiveScene().buildIndex;
    }
}
