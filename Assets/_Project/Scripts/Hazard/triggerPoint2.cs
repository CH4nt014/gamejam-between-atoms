using UnityEngine;

public class triggerPoint2 : MonoBehaviour
{
    private Transform m_body;
    private Rigidbody2D m_rb;
   
    private void Awake()
    {
        m_body = transform.parent.Find("Body");
        if (m_body != null)
        {
            m_rb = m_body.GetComponent<Rigidbody2D>();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            m_rb.gravityScale = 1.0f;
        }
    }
}
