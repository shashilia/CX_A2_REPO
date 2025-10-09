using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    [SerializeField] private TMP_Text _scoreTextUI;

    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _scoreTextUI.text = Score.ToString();
        }
    }

    private Utilities.GameState _state;

    public Utilities.GameState State
    {
        get => _state;
        set
        {
            _state = value;
            _pauseUI.enabled = State == Utilities.GameState.Pause;
            _gameOverUI.enabled = State == Utilities.GameState.GameOver;
        }
    }

    [SerializeField] private TMP_Text _pauseUI;
    [SerializeField] private TMP_Text _gameOverUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("New instance initialized...");

            DontDestroyOnLoad(gameObject);
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate instance found and deleted...");
        }
    }

    private void Start()
    {
        State = Utilities.GameState.Play;
        _pauseUI.enabled = false;
        _gameOverUI.enabled = false;
        Score = 0;
    }

    private void Update()
    {
        switch (State)
        {
            case Utilities.GameState.Play:
                if (Input.GetKeyDown(KeyCode.P))
                    State = Utilities.GameState.Pause;
                break;

            case Utilities.GameState.Pause:
                if (Input.GetKeyDown(KeyCode.P))
                    State = Utilities.GameState.Play;
                break;

            case Utilities.GameState.GameOver:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    State = Utilities.GameState.Play;
                    Score = 0;
                }
                break;
        }
    }

    public void ScorePoint()
    {
        Score ++;
    }

}
