using UnityEngine;

public class PeddleBehavior : MonoBehaviour
{
    [Range(1.0f, 10.0f)]
    public float Speed = 5.0f;
    [SerializeField] private KeyCode _leftDirection;
    [SerializeField] private KeyCode _rightDirection;

    private float _direction;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.linearVelocityX = _direction * Speed;
    }

    void Update()
    {
        _direction = 0.0f;

        if (GameBehavior.Instance.State == Utilities.GameState.Play)
        {
            if (Input.GetKey(_rightDirection))
                _direction += 1.0f;

            if (Input.GetKey(_leftDirection))
                _direction -= 1.0f;
        }

        if (GameBehavior.Instance.State == Utilities.GameState.GameOver)
        {
            transform.position = new Vector3(0, -4, 0);
            _direction = 0.0f;
        }

    }

}
