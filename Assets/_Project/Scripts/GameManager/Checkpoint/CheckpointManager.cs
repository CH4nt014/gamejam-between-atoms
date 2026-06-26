using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    public Vector3 PlayerRespawnPosition { get; private set; }
    public Vector3 DogRespawnPosition { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private Transform dog;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerRespawnPosition = player.position;
        DogRespawnPosition = dog.position;
    }

    public void SetCheckpoint(Vector3 playerPosition, Vector3 dogPosition)
    {
        PlayerRespawnPosition = playerPosition;
        DogRespawnPosition = dogPosition;

        Debug.Log("Checkpoint saved!");
    }
}