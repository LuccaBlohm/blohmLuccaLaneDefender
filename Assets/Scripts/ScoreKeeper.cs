using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    /// <summary>
    /// variabkes used throughout the script
    /// </summary>
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

    /// <summary>
    /// setters and getters for variables that will be used in other scripts
    /// </summary>
    public float Score { get => score; set => score = value; }
    public float Lives { get => lives; set => lives = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }

    /// <summary>
    /// on start display the current scores and lives
    /// </summary>
    void Start()
    {
        scoreText.text = "Score: " + Score;
        livesText.text = "Lives: " + Lives;

        checkScore = false;
    }

    /// <summary>
    /// whenever the game is over take the current score and set it as a PlayerPref then compare the new score with the
    /// current high score and if its bigger set that as the new high score. Then display score and high score.
    /// </summary>
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

    /// <summary>
    /// return to main menu
    /// </summary>
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// check for the lives and score at all times and if the game is over then generate final score
    /// </summary>
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
