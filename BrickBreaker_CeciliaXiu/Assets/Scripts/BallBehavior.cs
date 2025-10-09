using UnityEngine;
using UnityEngine.Audio;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _launchForce = 5.0f;
    [SerializeField] private float _paddleInfluence = 0.4f;
    private Rigidbody2D _rb;

    [SerializeField] private AudioResource _paddleHit;
    [SerializeField] private AudioResource _wallHit;
    [SerializeField] private AudioResource _gameOver;
    [SerializeField] private AudioResource _brickHit;

    private AudioSource _source;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();
        ResetBall();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            if (!Mathf.Approximately(other.rigidbody.linearVelocity.y, 0.0f))
            {
                Vector2 direction = _rb.linearVelocity * (1 - _paddleInfluence)
                                    + other.rigidbody.linearVelocity * _paddleInfluence;
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized;
            }
            _source.resource = _paddleHit;
        }

        else if(other.gameObject.CompareTag("Brick"))
            _source.resource = _brickHit;

        else
            _source.resource = _wallHit;

        _source.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ResetBall();
        GameBehavior.Instance.State = Utilities.GameState.GameOver;
        _source.resource = _gameOver;
        _source.Play();
    }

    private void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;

        transform.position = Vector3.zero;

        Vector2 direction = new Vector2(
            GetNonZeroRandomFloat(),
            GetNonZeroRandomFloat()
        ).normalized;

        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }

    float GetNonZeroRandomFloat(float min = -1.0f, float max = 1.0f)
    {
        float num;

        do
        {
            num = Random.Range(min, max);
        } while (Mathf.Approximately(num, 0.0f));

        return num;
    }

    void Update()
    {
        _rb.simulated = GameBehavior.Instance.State == Utilities.GameState.Play;
    }

}
