using UnityEngine;

public class GasCloud1 : MonoBehaviour
{
    [Header("Gas Cloud")]

    [SerializeField] private int speedReduction;

    PlayerController playerController;
    CircleCollider2D m_circleCollider;
    
    private float originalWalkSpeed;
    private float originalRunSpeed;

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

            originalWalkSpeed = playerController.walkSpeed;
            originalRunSpeed = playerController.runSpeed;

            playerController.walkSpeed = originalWalkSpeed - ((originalWalkSpeed / 100) * speedReduction);
            playerController.runSpeed = originalRunSpeed - ((originalRunSpeed / 100) * speedReduction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerController.walkSpeed = originalWalkSpeed;
            playerController.runSpeed = originalRunSpeed;
        }
    }
}
