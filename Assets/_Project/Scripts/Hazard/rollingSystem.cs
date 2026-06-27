using UnityEngine;

public class rollingSystem : MonoBehaviour
{
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rollingSpeed;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    Rigidbody2D m_rb;

    PlayerController playerController;

    private void Awake()
    {
        m_rb = transform.GetComponent<Rigidbody2D>();
        groundCheckRadius = 0.6f;
        rollingSpeed = 3f;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);


        if (isGrounded)
        {
            Vector3 targetPosition = transform.position + Vector3.left * Time.deltaTime * rollingSpeed; 
            m_rb.MovePosition(targetPosition);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player"))
            return;
            
        Debug.Log("Tube hit Player");
        playerController = collision.transform.GetComponent<PlayerController>();
    }


}
