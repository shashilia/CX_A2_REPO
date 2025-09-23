using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                Destroy(gameObject);
            }
        }

    void Update()
    {

    }
}
