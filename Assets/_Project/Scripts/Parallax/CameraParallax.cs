using UnityEngine;

public class CameraParallax : MonoBehaviour
{

    public GameObject Player;

    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
    }
}
