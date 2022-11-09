using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject buttonStart;
    [SerializeField] private GameObject buttonRestart;
    private int _scorePoints;
    private float _timerCount = 60;
    private SpawnManager _spawnManager;
    public bool IsGameStarted { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (_timerCount > 0 && IsGameStarted)
        {
            _timerCount -= Time.deltaTime;
            timerText.SetText("Time: " + Mathf.RoundToInt(_timerCount));
        }
        else if (_timerCount < 0)
        {
            IsGameStarted = false;
            gameOverText.gameObject.SetActive(true);
            buttonRestart.SetActive(true);
            _spawnManager.SpawnObjects(false);
        }
    }

    public void AddScore()
    {
        _scorePoints++;
        scoreText.SetText("Score: " + _scorePoints);
    }

    public void StartGame()
    {
        IsGameStarted = true;
        _scorePoints = 0;
        scoreText.SetText("Score: " + _scorePoints);
        timerText.SetText("Time: " + Mathf.RoundToInt(_timerCount));
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        buttonStart.SetActive(false);
        _spawnManager.SpawnObjects(true);
        titleText.gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        buttonRestart.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        _timerCount = 60;
        _scorePoints = 0;
        scoreText.SetText("Score: " + _scorePoints);
        timerText.SetText("Time: " + Mathf.RoundToInt(_timerCount));
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        _spawnManager.SpawnObjects(true);
        IsGameStarted = true;
        DestroyAllWaste();
    }

    private void DestroyAllWaste()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("Waste");
        foreach (var t in array)
        {
            Destroy(t);
        }
    }
}