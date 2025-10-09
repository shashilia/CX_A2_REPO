using UnityEngine;
using UnityEngine.Audio;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 2;
    private int _health;

    [SerializeField] private AudioResource _scorePoint;
    [SerializeField] private AudioResource _brickHit;
    private AudioSource _source;
    private Collider2D _collider;

    [SerializeField] private Color[] _colors = {new Color(0.0f, 0.36f, 0.73f), new Color(0.38f, 0.74f, 0.94f)};
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _health = _maxHealth;
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
        UpdateColor();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            _health--;

            if (_health <= 0)
            {
                GameBehavior.Instance.ScorePoint();
                _collider.enabled = false;
                _spriteRenderer.enabled = false;
                _source.resource = _scorePoint;

            }
            else
            {
                _source.resource = _brickHit;
                UpdateColor();
            }

        _source.Play();
        }
    }

    private void Update()
    {
        if (GameBehavior.Instance.State == Utilities.GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            ResetBrick();
        }
    }

    private void UpdateColor()
    {
        int index = Mathf.Clamp(_maxHealth - _health, 0, _colors.Length - 1);
        _spriteRenderer.color = _colors[index];
    }

    public void ResetBrick()
    {
        _health = _maxHealth;
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
        UpdateColor();
    }
}
