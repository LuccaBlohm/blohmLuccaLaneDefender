using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private float lives = 3;
    private float highScore;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;

    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private TMP_Text scoreNum;
    [SerializeField] private TMP_Text highScoreNum;
    [SerializeField] private bool gameOver;

    private bool checkScore;

    public float Score { get => score; set => score = value; }
    public float Lives { get => lives; set => lives = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + Score;
        livesText.text = "Lives: " + Lives;

        checkScore = false;
    }

    private void GenerateScore()
    {
        PlayerPrefs.SetFloat("NewScore", Score);
        if (PlayerPrefs.GetFloat("NewScore") >= PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("NewScore"));
        }
        highScore = PlayerPrefs.GetFloat("HighScore");
        scoreNum.text = "" + Score;
        highScoreNum.text = "" + highScore;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Score;
        livesText.text = "Lives: " + Lives;

        if (lives <= 0)
        {
            gameOver = true;
            if (!checkScore)
            {
                checkScore = true;
                GenerateScore();
            }
            scoreScreen.SetActive(true);
        }
    }
}
