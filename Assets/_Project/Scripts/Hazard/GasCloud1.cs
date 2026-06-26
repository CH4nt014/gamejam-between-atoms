using UnityEngine;

public class GasCloud1 : MonoBehaviour
{
    [Header("Gas Cloud")]

    [SerializeField] private int speedReduction;

    PlayerController playerController;
    CircleCollider2D m_circleCollider;
    float originalSpeed;

    private void Awake()
    {
        //in percentuale 
        speedReduction = 50;

        m_circleCollider = transform.GetComponent<CircleCollider2D>();
        m_circleCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            originalSpeed = playerController.walkSpeed;
            playerController.walkSpeed = originalSpeed - ((originalSpeed / 100) * speedReduction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerController.walkSpeed = originalSpeed;
        }
    }
}
