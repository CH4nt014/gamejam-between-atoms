using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Events")]
    [SerializeField] private UnityEvent onWin;
    [SerializeField] private UnityEvent onLose;

    [Header("UI")]
    [SerializeField] private GameObject pauseMenu;

    [Header("Game References")]
    [Tooltip("Drag your GameTimer object here so the GameManager can read it.")]
    [SerializeField] private GameTimer gameTimer;

    public bool IsGameOver { get; private set; }
    public bool IsVictory { get; private set; }
    public bool IsPaused { get; private set; }

    // This holds the exact final time for your Victory UI to read
    public float FinalTime { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Win()
    {
        if (IsGameOver || IsVictory) return;

        IsVictory = true;

        SaveTimer();

        onWin.Invoke();
        Debug.Log($"VICTORY - Final Time: {FinalTime}");
    }

    public void GameOver()
    {
        if (IsGameOver || IsVictory) return;

        IsGameOver = true;

        // If the game timer is running, stop it, but DO NOT save the time.
        if (gameTimer != null)
        {
            gameTimer.StopTimer();
        }

        onLose.Invoke();
        Debug.Log("GAME OVER");
    }

    private void SaveTimer()
    {
        if (gameTimer != null)
        {
            // Tell the timer to stop counting
            gameTimer.StopTimer();

            // Store the exact time
            FinalTime = gameTimer.ExactTime;

            // Save the Last Run (Optional, good for "Previous Run" stats)
            PlayerPrefs.SetFloat("LastRunTime", FinalTime);

            // Check and Save BEST Run
            // Grab the current best time. If none exists, default to a massive number so the first run always wins.
            float currentBest = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

            // If the player beat their record, save the new record!
            if (FinalTime < currentBest)
            {
                PlayerPrefs.SetFloat("BestTime", FinalTime);
                Debug.Log($"NEW RECORD! {FinalTime} seconds!");
            }

            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("GameTimer is missing! Did you forget to drag it into the GameManager Inspector?");
        }
    }

    public void TogglePause()
    {
        if (IsGameOver || IsVictory) return;

        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(IsPaused);
        }

        Debug.Log(IsPaused ? "PAUSED" : "RESUMED");
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }
}