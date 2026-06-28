using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [Tooltip("Scene loaded when the game over animation finishes.")]
    [SerializeField] private string m_nextSceneName = "MainMenu";

    [Tooltip("Fallback delay before returning to the menu, in case the Animation Event is not wired. Set <= 0 to disable.")]
    [SerializeField] private float m_fallbackDelay = -1f;

    private bool m_transitionStarted;

    private void Awake()
    {
        // GameManager.GameOver() sets Time.timeScale = 0 right after invoking onLose,
        // and that value carries over into this scene, freezing the Animator.
        // Reset it here so the game over animation plays.
        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (m_fallbackDelay > 0f)
            Invoke(nameof(GoToMenu), m_fallbackDelay);
    }

    // Call this from an Animation Event on the last frame of the game over clip.
    public void GoToMenu()
    {
        if (m_transitionStarted) return;
        m_transitionStarted = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene(m_nextSceneName);
    }
}
