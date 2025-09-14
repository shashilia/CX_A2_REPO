using UnityEngine;

public class PeddleBehavior : MonoBehaviour
{
    public float Speed = 5.0f;
    public KeyCode LeftDirection;
    public KeyCode RightDirection;

    //Setting the border
    public float leftLimit  = -8.72f;
    public float rightLimit = 8.72f;

    void Start()
    {

    }

    void Update()
    {
        float movement = 0.0f;

        if (Input.GetKey(LeftDirection))
        {
            movement -= Speed;
        }

        if (Input.GetKey(RightDirection))
        {
            movement += Speed;
        }

        float newX = transform.position.x + movement * Time.deltaTime;
        newX = Mathf.Clamp(newX, leftLimit, rightLimit);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

}
