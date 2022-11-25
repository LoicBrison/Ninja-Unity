using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button startButton;
    public List<GameObject> targets;
    public List<GameObject> hearts;
    private float spawnRate = 1.0f;
    private int score;
    private int life;
    public bool isGameActive = false;
    public bool isGameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        gameOverText = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        gameOverText.gameObject.SetActive(false);
        score = 0;
        life = 3;
        UpdateScore(0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (isGameStart)
        {
            RestartGame();
            return;
        }
        isGameStart = true;
        isGameActive = true;
        startButton.gameObject.SetActive(false);
        StartCoroutine(SpawnTarget());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLife(){
        if (isGameActive)
        {
            hearts[life-1].SetActive(false);
            life--;
            if (life == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        startButton.gameObject.SetActive(true);
        startButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Try again";
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    
}
