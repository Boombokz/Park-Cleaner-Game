using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject buttonStart;
    [SerializeField] private GameObject buttonRestart;
    private int scorePoints = 0;
    private float timerCount = 60;
    private SpawnManager spawnManager;
    public bool isGameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (timerCount > 0 && isGameStarted)
        {
            timerCount -= Time.deltaTime;
            timerText.SetText("Time: " + Mathf.RoundToInt(timerCount));
        }
        else if (timerCount < 0)
        {
            isGameStarted = false;
            gameOverText.gameObject.SetActive(true);
            buttonRestart.SetActive(true);
            spawnManager.SpawnObjects(false);
        }
    }

    public void AddScore()
    {
        scorePoints++;
        scoreText.SetText("Score: " + scorePoints);
    }

    public void StartGame()
    {
        isGameStarted = true;
        scorePoints = 0;
        scoreText.SetText("Score: " + scorePoints);
        timerText.SetText("Time: " + Mathf.RoundToInt(timerCount));
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        buttonStart.SetActive(false);
        spawnManager.SpawnObjects(true);
        titleText.gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        buttonRestart.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        timerCount = 60;
        scorePoints = 0;
        scoreText.SetText("Score: " + scorePoints);
        timerText.SetText("Time: " + Mathf.RoundToInt(timerCount));
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        spawnManager.SpawnObjects(true);
        isGameStarted = true;
        DestroyAllWaste();
    }

    private void DestroyAllWaste()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("Waste");
        for (int i = 0; i < array.Length; i++)
        {
            Destroy(array[i]);
        }
    }
}