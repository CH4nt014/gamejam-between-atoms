using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class UnstableIsotope : MonoBehaviour
{

    [SerializeField] private float movemetRadius = 0.5f;
    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private bool playerInRange = false;

    Vector3 startPosition;

    float seedX;
    float seedY;

    [SerializeField] private float index = 3;
    [SerializeField] private float counter = 0;
    [SerializeField] private bool startCount;

    void Awake()
    {
        startPosition = transform.position;

        seedX = Random.Range(0f, 1000f);
        seedY = Random.Range(0f, 1000f);

        index = 3;
        counter = 0;

        movementSpeed = 0.4f;
        movemetRadius = 1.5f;
    }

    private void Update()
    {
        float time = Time.time * movementSpeed;

        float x = Mathf.PerlinNoise(seedX, time) * 2f - 1f;
        float y = Mathf.PerlinNoise(seedY, time) * 2f - 1f;

        Vector2 movement = new Vector2(x, y);

        movement = Vector2.ClampMagnitude(movement, 1f);

        transform.localPosition = startPosition + (Vector3)(movement*movemetRadius);


        if (startCount)
        {
            counter += Time.deltaTime;
        }

        if (counter >= index)
        {
            if (playerInRange)
            {
                //perform player death
                Debug.Log("Player death");
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            playerInRange = true;
            startCount = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
