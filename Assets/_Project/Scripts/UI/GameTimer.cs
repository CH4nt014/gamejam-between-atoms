using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Tooltip("The TextMeshPro label that displays the timer.")]
    [SerializeField] private TMP_Text m_label;

    [Tooltip("Show as MM:SS when true, otherwise show raw seconds.")]
    [SerializeField] private bool m_useMinutesFormat = true;

    [Tooltip("Include milliseconds (e.g. MM:SS.ms) for speedruns.")]
    [SerializeField] private bool m_showMilliseconds = true;

    [Tooltip("Start counting automatically on Start.")]
    [SerializeField] private bool m_autoStart = true;

    private float m_elapsed;
    private int m_lastShownSecond = -1;
    private bool m_running;

    private void Start()
    {
        if (m_autoStart) StartTimer();
    }

    public void StartTimer()
    {
        m_elapsed = 0f;
        m_lastShownSecond = -1;
        m_running = true;
        UpdateLabel(); // Show 0 immediately
    }

    public void StopTimer()
    {
        m_running = false;
    }

    /// <summary>Elapsed time in whole seconds (useful for scoring).</summary>
    public int ElapsedSeconds => Mathf.FloorToInt(m_elapsed);

    /// <summary>Exact elapsed time including fractions of a second.</summary>
    public float ExactTime => m_elapsed;

    private void Update()
    {
        if (!m_running) return;

        m_elapsed += Time.deltaTime;

        if (m_showMilliseconds)
        {
            // If we want milliseconds, we MUST update the UI every frame.
            UpdateLabel();
        }
        else
        {
            // Old optimization: Update once per second to save performance.
            int currentSecond = Mathf.FloorToInt(m_elapsed);
            if (currentSecond != m_lastShownSecond)
            {
                m_lastShownSecond = currentSecond;
                UpdateLabel();
            }
        }
    }

    private void UpdateLabel()
    {
        if (m_label == null) return;

        // Calculate milliseconds
        int ms = Mathf.FloorToInt((m_elapsed % 1f) * 1000f);

        // This tag forces every character to be exactly 0.6em wide.
        // (You might need to tweak "0.6" to "0.5" or "0.7" depending on your specific font!)
        string mono = "<mspace=0.6em>";

        if (m_useMinutesFormat)
        {
            int minutes = Mathf.FloorToInt(m_elapsed / 60f);
            int seconds = Mathf.FloorToInt(m_elapsed % 60f);

            if (m_showMilliseconds)
            {
                m_label.text = $"{mono}{minutes:00}:{seconds:00}.{ms:000}</mspace>";
            }
            else
            {
                m_label.text = $"{mono}{minutes:00}:{seconds:00}</mspace>";
            }
        }
        else
        {
            int totalSeconds = Mathf.FloorToInt(m_elapsed);

            if (m_showMilliseconds)
            {
                m_label.text = $"{mono}{totalSeconds}.{ms:000}</mspace>";
            }
            else
            {
                m_label.text = $"{mono}{totalSeconds}</mspace>";
            }
        }
    }
}