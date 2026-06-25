using UnityEngine;

public class DogCatchDetector : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private DogController dogController;

    private void Awake()
    {
        dogController = GetComponent<DogController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered: " + other.name);

        if (!other.CompareTag(playerTag))
            return;

        Debug.Log("Dog Caught!");

        dogController.StopDog();
    }
}