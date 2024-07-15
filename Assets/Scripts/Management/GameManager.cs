using Assets.Scripts.Enums;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameState currentState;
    private GameState previousState;

    [Header("UI")]
    [SerializeField]
    private GameObject pauseScreen;

    // Reference to the game object you want to save
    [SerializeField]
    private GameObject gameObjectToSave;

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

    // Function to save the game
    public void SaveGame()
    {
        DataManagement.Instance.SaveGame();   
    }

    // Function to load the saved game
    public void LoadGame()
    {
        DataManagement.Instance.LoadGame();
    }

    public void NewGame()
    {
        DataManagement.Instance.NewGame();
    }

    public void DisableScreen()
    {
        pauseScreen.SetActive(false);
    }
}
