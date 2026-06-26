using UnityEngine;

public class DogOffScreenLoseCondition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxOffScreenTime = 10f;

    private float offScreenTimer;
    private bool hasTriggeredGameOver;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if (hasTriggeredGameOver) return;

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        bool isOutsideScreen =
            viewportPosition.x < 0f ||
            viewportPosition.x > 1f ||
            viewportPosition.y < 0f ||
            viewportPosition.y > 1f;

        if (!isOutsideScreen)
        {
            offScreenTimer = 0f;
            return;
        }

        offScreenTimer += Time.deltaTime;

        Debug.Log($"Dog off-screen: {offScreenTimer:F1}s");

        if (offScreenTimer >= maxOffScreenTime)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        hasTriggeredGameOver = true;
        GameManager.Instance.GameOver();
    }
}