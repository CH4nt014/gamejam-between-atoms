using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform dogCheckpointPosition;

    private bool isActivated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActivated) return;
        if (!other.CompareTag("Player")) return;

        Vector3 playerPosition = transform.position;
        Vector3 dogPosition = dogCheckpointPosition.position;

        CheckpointManager.Instance.SetCheckpoint(playerPosition, dogPosition);

        isActivated = true;
    }
}