using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text scoreText;

    public TMP_Text gameOver;

    public Button restartButton;

    int score = 0;

    void Awake()
    {
        instance = this;
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(restartGame);
    }

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void Update()
    {
        
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void showGameOver()
    {
        gameOver.text = "Game Over";
        restartButton.gameObject.SetActive(true);
    }

    public void restartGame()
    {
        gameOver.text = "";
        restartButton.gameObject.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        FishSpawner.instance.gameActive = true;
        FishSpawner.instance.Start();
    }
}
