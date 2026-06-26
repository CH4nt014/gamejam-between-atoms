using UnityEngine;

public class DogCatchDetector : MonoBehaviour
{
    private DogController dogController;

    private void Awake()
    {
        dogController = GetComponentInParent<DogController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Dog caught!");
        dogController.StopDog();
    }
}