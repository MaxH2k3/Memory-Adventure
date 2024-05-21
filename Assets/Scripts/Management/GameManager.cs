using Assets.Scripts.Enums;
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

    private new void Awake()
    {
        base.Awake();

        DisableScreen();
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

    public void DisableScreen()
    {
        pauseScreen.SetActive(false);
    }
}
