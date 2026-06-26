using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private Transform dog;

    private void Awake()
    {
        Instance = this;
    }

    public void Respawn()
    {
        player.position = CheckpointManager.Instance.PlayerRespawnPosition;
        dog.position = CheckpointManager.Instance.DogRespawnPosition;
        dog.GetComponent<DogOffScreenLoseCondition>().ResetTimer();
        Debug.Log("Respawn!");
    }
}