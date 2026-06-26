using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }
    public bool IsVictory { get; private set; }
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Time.timeScale = 1f;
    }

    public void Win()
    {
        if (IsGameOver || IsVictory) return;

        IsVictory = true;
        Time.timeScale = 0f;
        Debug.Log("VICTORY");
    }

    public void GameOver()
    {
        if (IsGameOver || IsVictory) return;

        IsGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("GAME OVER");
    }

    public void TogglePause()
    {
        if (IsGameOver || IsVictory) return;

        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        Debug.Log(IsPaused ? "PAUSED" : "RESUMED");
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
